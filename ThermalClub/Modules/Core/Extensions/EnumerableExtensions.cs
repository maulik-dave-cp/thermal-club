﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using CsvHelper.Configuration;

namespace ThermalClub.Modules.Core.Extensions
{
	public static class EnumerableExtensions
	{
		/// <summary>
		/// Does a list contain all values of another list?
		/// </summary>
		/// <remarks>Needs .NET 3.5 or greater.  Source:  https://stackoverflow.com/a/1520664/1037948 </remarks>
		/// <typeparam name="T">list value type</typeparam>
		/// <param name="containingList">the larger list we're checking in</param>
		/// <param name="lookupList">the list to look for in the containing list</param>
		/// <returns>true if it has everything</returns>
		public static bool ContainsAll<T>(this IEnumerable<T> containingList, IEnumerable<T> lookupList)
		{
			return !lookupList.Except(containingList).Any();
		}

		public static string GetDescription<T>(this T enumerationValue) where T : struct
		{
			var type = enumerationValue.GetType();
			if (!type.IsEnum)
			{
				throw new ArgumentException($"{nameof(enumerationValue)} must be of Enum type", nameof(enumerationValue));
			}
			var memberInfo = type.GetMember(enumerationValue.ToString());
			if (memberInfo.Length > 0)
			{
				var attrs = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

				if (attrs.Length > 0)
				{
					return ((DescriptionAttribute)attrs[0]).Description;
				}
			}
			return enumerationValue.ToString();
		}

		public static string ToCsv<T, TMap>(this IEnumerable<T> collection) where TMap : ClassMap
		{
			using (var memoryStream = new MemoryStream())
			{
				//using (var streamWriter = new StreamWriter(memoryStream))
				//{
				//	using (var csvWriter = new CsvWriter(streamWriter))
				//	{
				//		csvWriter.Configuration.RegisterClassMap<TMap>();
				//		csvWriter.WriteRecords(collection);
				//	}
				//}

				return Encoding.ASCII.GetString(memoryStream.ToArray());
			}
		}

		public static bool TryParseEnum<TEnum>(this int enumValue, out TEnum retVal)
		{
			retVal = default(TEnum);
			var success = Enum.IsDefined(typeof(TEnum), enumValue);
			if (success)
			{
				retVal = (TEnum)Enum.ToObject(typeof(TEnum), enumValue);
			}
			return success;
		}
	}
}