namespace Application.Constants;

[Flags]
public enum AccountRole
{
    /* 0 x 0 0
    /      ^ ^
    /      | GroupCreationAllowed
    /      |
    /      StudentCreationAllowed
    */

    // 0x00
    RegularAdministrator = 0,

    // 0x01
    IsuAdministrator = 1,

    // 0x10
    FacultyAdministrator = 2,

    // 0x11
    SuperAdministrator = 3,
}