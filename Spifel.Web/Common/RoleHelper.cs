namespace Spifel.Web.Common
{
    public class RoleHelper
    {
        public static string GetRoleLabelClass(string role)
        {
            return role switch
            {
                "Admin" => "label-danger",
                "Developer" => "label-info",
                "Accountant" => "label-success",
                "HR" => "label-inverse",
                _ => "label-default"
            };
        }
    }
}
