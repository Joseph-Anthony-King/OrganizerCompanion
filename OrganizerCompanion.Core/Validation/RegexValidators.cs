namespace OrganizerCompanion.Core.Validation
{
    internal static class RegexValidators
    {
        // Email must be in a valid format
        internal const string EmailRegexPattern = @"(^\w+([.-]?\w+)*@\w+([.-]?\w+)*(\.\w{2,3})+$)";
        // Guid must be in the pattern of d36ddcfd-5161-4c20-80aa-b312ef161433 with hexadecimal characters
        internal const string GuidRegexPattern = @"(^([0-9A-Fa-f]{8}[-][0-9A-Fa-f]{4}[-][0-9A-Fa-f]{4}[-][0-9A-Fa-f]{4}[-][0-9A-Fa-f]{12})$)";
        /* Password must be from 4 and up through 20 characters with at least 1 upper case letter, 1 lower case letter, 1 numeric character, 
         * and 1 special character of ! @ # $ % ^ & * + = ? - _ . , */
        internal const string PasswordRegexPattern = @"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&*+=?\-_.,]).{4,20}$";
        /* User name must be at least 4 characters and can contain alphanumeric characters and special characters of
         * [! @ # $ % ^ & * + = ? - _ . ,] */
        internal const string UserNameRegexPattern = @"^[a-zA-Z0-9!@#$%^&*+=<>?_.,-]{4,}$";
        // Must be a valid url with an http or https protocol
        internal const string UrlRegexPattern = @"^(ht|f)tp(s?)\:\/\/[0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*(:(0-9)*)*(\/?)([a-zA-Z0-9\-\.\?\,\'\/\\\+&%\$#_=]*)?$";
        // Must be a valid NANP phone number (US, Canada, Mexico, and Caribbean) with optional country code, area code, and extensions
        internal const string NANPPhoneNumberRegexPattern = @"^(\+(1|52)[- ]?)?(\(?\d{2,3}\)?[- ]?)?[\d\- ]{7,10}(\s*(ext|x|extension)\s*\d{1,5})?$";
        /* Must be a valid SQL Server database connection string with common keywords including:
         * Server, Data Source, Initial Catalog, Database, User ID, UID, Password, PWD, 
         * Integrated Security, Trusted_Connection, Encrypt, TrustServerCertificate,
         * Connection Timeout, Timeout, Application Name, MultipleActiveResultSets, Pooling,
         * Min Pool Size, Max Pool Size, and other common SQL Server connection parameters */
        internal const string SQLServerDbConnectionStringRegexPattern = @"^([a-zA-Z][\w\s]*=[^;]*(;|$))+$";
        internal const string SQLiteDbConnectionStringRegexPattern = @"^Data Source=[^;]+(;Version=\d+)?(;Cache=(Shared|Private))?(;Mode=(ReadOnly|ReadWrite|ReadWriteCreate))?(;Password=[^;]+)?(;Pooling=(True|False))?(;Max Pool Size=\d+)?(;Min Pool Size=\d+)?(;Synchronous=(Off|Normal|Full))?(;Journal Mode=(Delete|Truncate|Persist|Memory|WAL))?(;Foreign Keys=(True|False))?(;Busy Timeout=\d+)?$";
        internal const string MySQLDbConnectionStringRegexPattern = @"^((Server|Data Source|Host|Address|Addr|Network)=.+;)?((Port)=\d+;)?((Database|Initial Catalog)=.+;)?((User ID|UID)=.+;)?((Password|PWD)=.+;)?((Pooling)=(True|False);)?((Min Pool Size)=\d+;)?((Max Pool Size)=\d+;)?((Connection Timeout|Connect Timeout)=\d+;)?((Ssl Mode)=(None|Preferred|Required|VerifyCA|VerifyFull);)?((Charset)=.+;)?((Allow User Variables)=(True|False);)?((Convert Zero Datetime)=(True|False);)?((Default Command Timeout)=\d+;)?$";
        internal const string PostgreSQLDbConnectionStringRegexPattern = @"^((Host|Server)=.+;)?((Port)=\d+;)?((Database|Initial Catalog)=.+;)?((User ID|Username|UID)=.+;)?((Password|PWD)=.+;)?((Pooling)=(True|False);)?((Min Pool Size)=\d+;)?((Max Pool Size)=\d+;)?((Timeout|Connection Timeout|Command Timeout)=\d+;)?((SSL Mode)=(Disable|Allow|Prefer|Require|VerifyCA|VerifyFull);)?((Search Path)=.+;)?((Application Name)=.+;)?$";
    }
}
