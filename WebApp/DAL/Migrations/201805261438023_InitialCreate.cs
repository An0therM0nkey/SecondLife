namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EducationDetails",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        CertificateDegreeName = c.String(nullable: false, maxLength: 128),
                        Major = c.String(nullable: false, maxLength: 128),
                        UserID = c.String(maxLength: 128),
                        InstituteUniversityName = c.String(),
                        StartingDate = c.DateTime(nullable: false, storeType: "date"),
                        CompletionDate = c.DateTime(storeType: "date"),
                    })
                .PrimaryKey(t => new { t.Id, t.CertificateDegreeName, t.Major })
                .ForeignKey("dbo.AspNetUsers", t => t.UserID)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.ExperienceDetails",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        StartDate = c.DateTime(nullable: false, storeType: "date"),
                        EndDate = c.DateTime(nullable: false, storeType: "date"),
                        UserID = c.String(maxLength: 128),
                        IsCurrentJob = c.Boolean(nullable: false),
                        JobTitle = c.String(),
                        CompanyName = c.String(),
                        JobLocationCity = c.String(),
                        JobLocationCountry = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => new { t.Id, t.StartDate, t.EndDate })
                .ForeignKey("dbo.AspNetUsers", t => t.UserID)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.JobLocations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StreetAddres = c.String(),
                        City = c.String(),
                        Country = c.String(),
                        Zip = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.JobPosts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PostedByID = c.String(maxLength: 128),
                        JobTypeID = c.Int(nullable: false),
                        CompanyName = c.String(),
                        CreatedDate = c.DateTime(nullable: false, storeType: "date"),
                        JobDescription = c.String(),
                        JobLocationID = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.JobLocations", t => t.JobLocationID, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.PostedByID)
                .Index(t => t.PostedByID)
                .Index(t => t.JobLocationID);
            
            CreateTable(
                "dbo.JobTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.SeekerResumes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserID = c.String(maxLength: 128),
                        FirstName = c.String(),
                        LastName = c.String(),
                        CurrentSalary = c.Double(),
                        IsAnnuallyMonthly = c.String(),
                        Currency = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserID)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.SkillSets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SeekerResumes", "UserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.JobPosts", "PostedByID", "dbo.AspNetUsers");
            DropForeignKey("dbo.JobPosts", "JobLocationID", "dbo.JobLocations");
            DropForeignKey("dbo.ExperienceDetails", "UserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.EducationDetails", "UserID", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.SeekerResumes", new[] { "UserID" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.JobPosts", new[] { "JobLocationID" });
            DropIndex("dbo.JobPosts", new[] { "PostedByID" });
            DropIndex("dbo.ExperienceDetails", new[] { "UserID" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.EducationDetails", new[] { "UserID" });
            DropTable("dbo.SkillSets");
            DropTable("dbo.SeekerResumes");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.JobTypes");
            DropTable("dbo.JobPosts");
            DropTable("dbo.JobLocations");
            DropTable("dbo.ExperienceDetails");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.EducationDetails");
        }
    }
}
