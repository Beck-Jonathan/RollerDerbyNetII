namespace DataAccessInterfaces
{
    public interface ISkater_Role_LineAccessor
    {
        int insertSkater_Role_Line(string RoleID, string SkaterID);
        int removeSkater_Role_Line(string RoleID, string SkaterID);

    }
}
