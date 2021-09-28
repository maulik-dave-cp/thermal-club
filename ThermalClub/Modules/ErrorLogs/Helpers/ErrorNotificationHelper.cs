namespace ThermalClub.Modules.ErrorLogs.Helpers
{
    public static class ErrorNotificationHelper
    {
        public static string GenerateTable(string description, string stacktrace)
        {
            return @$"
            <table style='width: 100%;padding: 20px 0 10px 0;'>
                <tbody>                    
                    <tr>
                        <td style='font-weight: bold;font-size: 14px;color: #5e5e5e;'>
                            Message  
                        </td>
                        <td>
                           {description} 
                        </td>
                    </tr>                    
                    <tr>
                        <td style='font-weight: bold;font-size: 14px;color: #5e5e5e;'>
                            Stack Trace 
                        </td>
                        <td>
                           {stacktrace} 
                        </td>
                    </tr>
                </tbody>
            </table>
            ";
        }
    }
}