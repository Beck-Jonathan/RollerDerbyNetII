namespace ASPPresentation.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddedFirstNameLastNamePhoneandSkaterIDtoUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "GivenName", c => c.String());
            AddColumn("dbo.AspNetUsers", "FamilyName", c => c.String());
            AddColumn("dbo.AspNetUsers", "SkaterID", c => c.String());
            AddColumn("dbo.AspNetUsers", "Phone", c => c.String());
        }

        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Phone");
            DropColumn("dbo.AspNetUsers", "SkaterID");
            DropColumn("dbo.AspNetUsers", "FamilyName");
            DropColumn("dbo.AspNetUsers", "GivenName");
        }
    }
}
