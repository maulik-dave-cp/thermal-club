using Microsoft.EntityFrameworkCore;

namespace ThermalClub.Modules.Core.Data
{
	public interface INestedSet
	{
		
	}

	public static class NestedSet
	{
		public static void BuildTree(this SqlContext sqlContext, string tableName, string extraCondition = null)
		{
            var extraWhereCondition = extraCondition.IsNotNullOrEmpty() ? "AND " + extraCondition : "";

            sqlContext.Database.ExecuteSqlRaw(
                string.Format(
            @"WITH DbLevels AS
			(
				SELECT
				Id,
				CONVERT(VARCHAR(MAX), Id) AS thePath,
				1 AS Level
				FROM [{0}]
				WHERE ParentId IS NULL {1}

				UNION ALL

				SELECT
				e.Id,
				x.thePath + '.' + CONVERT(VARCHAR(MAX), e.Id) AS thePath,
					x.Level + 1 AS Level
				FROM DbLevels x 
					JOIN [{0}] e on e.ParentId = x.Id {1}
					),
				DbRows AS
				(
					SELECT
						DbLevels.*,
				ROW_NUMBER() OVER (ORDER BY thePath) AS Row
				FROM DbLevels
					)
				UPDATE
					[{0}]
				SET
				[{0}].[Left] = (ER.Row * 2) - ER.Level,
				[{0}].[Right] = ((ER.Row * 2) - ER.Level) + 
										   (
											   SELECT COUNT(*) * 2
				FROM DbRows ER2 
					WHERE ER2.thePath LIKE ER.thePath + '.%'
					) + 1
				FROM
					DbRows AS ER
				WHERE [{0}].Id = ER.Id;", tableName, extraWhereCondition));
            
        }

		public static void MoveToParentNode(this INestedSet nestedSet, string tableName, int currentNodeId, int? newParentNodeId = null, string extraCondition = null)
		{
			//var extraWhereCondition = extraCondition.IsNotNullOrEmpty() ? "AND " + extraCondition : "";

			//using (var sqlContext = new SqlContext())
			//{
			//	// step 0: Initialize parameters.
			//	var sameParentIdExists = sqlContext.Database.SqlQuery<int?>(
			//		$"SELECT ParentId FROM {tableName} Where Id = {currentNodeId} {extraWhereCondition};", newParentNodeId).First();

			//	if (sameParentIdExists == newParentNodeId)
			//		return;

			//	var leftCurrentNode = sqlContext.Database.SqlQuery<int>($"SELECT [Left] FROM {tableName} Where Id = {currentNodeId} {extraWhereCondition};").First();
			//	var rightCurrentNode = sqlContext.Database.SqlQuery<int>($"SELECT [Right] FROM {tableName} Where Id = {currentNodeId} {extraWhereCondition};").First();

			//	var rightParentNode = newParentNodeId != null
			//		? sqlContext.Database.SqlQuery<int>($"SELECT [Right] FROM {tableName} Where Id = {newParentNodeId} {extraWhereCondition};").First()
			//		: 0;

			//	var nodeSize = rightCurrentNode - leftCurrentNode + 1;

			//	// step 1: temporary "remove" moving node
			//	sqlContext.Database.ExecuteSqlCommand($"UPDATE {tableName} SET [Left] = 0-([Left]), [Right] = 0-([Right]) WHERE [Left] >= {leftCurrentNode} AND [Right] <= {rightCurrentNode} {extraWhereCondition};");

			//	//step 2: decrease left and/or right position values of currently 'lower' AdminPermissionss (and parents)
			//	sqlContext.Database.ExecuteSqlCommand($"UPDATE {tableName} SET [Left] = [Left] - {nodeSize} WHERE [Left] > {rightCurrentNode} {extraWhereCondition};");
			//	sqlContext.Database.ExecuteSqlCommand($"UPDATE {tableName} SET [Right] = [Right] - {nodeSize} WHERE [Right] > {rightCurrentNode} {extraWhereCondition};");

			//	// step 3: increase left and/or right position values of future 'lower' AdminPermissionss (and parents)
			//	sqlContext.Database.ExecuteSqlCommand(
			//	$@"UPDATE {tableName}
			//		SET [Left] = [Left] + {nodeSize}
			//		WHERE [Left] >= CASE WHEN {rightParentNode} > {rightCurrentNode} THEN {rightParentNode} - {nodeSize} ELSE {rightParentNode} END {extraWhereCondition};");

			//	sqlContext.Database.ExecuteSqlCommand(
			//	$@"UPDATE {tableName}
			//		SET [Right] = [Right] + {nodeSize}
			//		WHERE [Right] >= CASE WHEN {rightParentNode} > {rightCurrentNode} THEN {rightParentNode} - {nodeSize} ELSE {rightParentNode} END {extraWhereCondition};");

			//	// step 4: move node (and it's subnodes)
			//	sqlContext.Database.ExecuteSqlCommand(
			//	$@"UPDATE {tableName}
			//		SET [Left] = 0-([Left]) + CASE WHEN {rightParentNode} > {rightCurrentNode} THEN {rightParentNode} - {rightCurrentNode} - 1 ELSE {rightParentNode} - {rightCurrentNode} - 1 + {nodeSize} END,
			//				  [Right] = 0-([Right]) + CASE WHEN {rightParentNode} > {rightCurrentNode} THEN {rightParentNode} - {rightCurrentNode} - 1 ELSE {rightParentNode} - {rightCurrentNode} - 1 + {nodeSize} END
			//		WHERE [Left] <= 0-{leftCurrentNode} AND [Right] >= 0-{rightCurrentNode} {extraWhereCondition};");

			//	// update it's parent AdminPermissions id
			//	sqlContext.Database.ExecuteSqlCommand($"UPDATE {tableName} SET ParentId = {newParentNodeId ?? (object)"NULL"} WHERE Id = {currentNodeId} {extraWhereCondition};");
			//}
		}

		internal sealed class TempDeleteDataEntity
		{
			public int NewLeft { get; set; }
			public int NewRight { get; set; }
			public int HasLeafs { get; set; }
			public int Width { get; set; }
			public int? SuperiorParent { get; set; }
		}

		public static void DeleteNode(this INestedSet nestedSet, string tableName, int nodeId, string extraCondition = null)
		{
			//var extraWhereCondition = extraCondition.IsNotNullOrEmpty() ? "AND " + extraCondition : "";

			//using (var sqlContext = new SqlContext())
			//{
			//	var tempDeleteDataEntity = sqlContext.Database.SqlQuery<TempDeleteDataEntity>(
			//		$@"SELECT [Left] AS NewLeft, [Right] As NewRight, ([Right] - [Left] - 1) as HasLeafs, ([Right] - [Left] + 1) as Width, ParentId As SuperiorParent
			//			FROM {tableName} WHERE Id = {nodeId}").First();

			//	sqlContext.Database.ExecuteSqlCommand($"DELETE FROM {tableName} WHERE Id = {nodeId} {extraWhereCondition};");

			//	if (tempDeleteDataEntity.HasLeafs == 0)
			//	{
			//		sqlContext.Database.ExecuteSqlCommand(
			//			$"DELETE FROM {tableName} WHERE [Left] BETWEEN {tempDeleteDataEntity.NewLeft} AND {tempDeleteDataEntity.NewRight} {extraWhereCondition};");

			//		sqlContext.Database.ExecuteSqlCommand(
			//			$"UPDATE {tableName} SET [Right] = [Right] - {tempDeleteDataEntity.Width} WHERE [Right] > {tempDeleteDataEntity.NewRight} {extraWhereCondition};");

			//		sqlContext.Database.ExecuteSqlCommand(
			//			$"UPDATE {tableName} SET [Left] = [Left] - {tempDeleteDataEntity.Width} WHERE [Left] > {tempDeleteDataEntity.NewRight} {extraWhereCondition};");
			//	}
			//	else
			//	{
			//		sqlContext.Database.ExecuteSqlCommand(
			//			$"DELETE FROM {tableName} WHERE [Left] = {tempDeleteDataEntity.NewLeft} {extraWhereCondition};");

			//		sqlContext.Database.ExecuteSqlCommand(
			//			$@"UPDATE {tableName} SET [Right] = [Right] - 1, [Left] = [Left] - 1, ParentId = {tempDeleteDataEntity.SuperiorParent} 
			//				WHERE [Left] BETWEEN {tempDeleteDataEntity.NewLeft} AND {tempDeleteDataEntity.NewRight} {extraWhereCondition};");

			//		sqlContext.Database.ExecuteSqlCommand(
			//			$"UPDATE {tableName} SET [Right] = [Right] - 2 WHERE [Right] > {tempDeleteDataEntity.NewRight} {extraWhereCondition};");

			//		sqlContext.Database.ExecuteSqlCommand(
			//			$"UPDATE {tableName} SET [Left] = [Left] - 2 WHERE [Left] > {tempDeleteDataEntity.NewRight} {extraWhereCondition};");
			//	}
			//}
		}
	}
}