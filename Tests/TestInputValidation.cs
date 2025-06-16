// Tests/TestInputValidation.cs
using System;
using System.Data;
using NUnit.Framework;

[TestFixture]
public class TestInputValidation {
    private DatabaseHelper dbHelper;
    private string testConnectionString = "Server=localhost\\SQLEXPRESS;Database=SafeVault;Trusted_Connection=True;"; // Updated for local SQL Server Express

    [SetUp]
    public void Setup() {
        dbHelper = new DatabaseHelper(testConnectionString);
    }

    [Test]
    public void TestForSQLInjection() {
        // Attempt SQL injection via username
        string maliciousInput = "' OR 1=1;--";
        DataTable result = dbHelper.GetUserByUsername(maliciousInput);
        // Should not return all users, should return 0 rows
        Assert.That(result.Rows.Count, Is.EqualTo(0), "SQL Injection should not succeed");
    }

    [Test]
    public void TestForXSS() {
        // Simulate XSS by injecting a script tag
        string xssInput = "<script>alert('xss')</script>";
        DataTable result = dbHelper.GetUserByUsername(xssInput);
        // Should not find a user with script tag in username
        Assert.That(result.Rows.Count, Is.EqualTo(0), "XSS payload should not be stored or found");
    }
}