using System.ComponentModel.DataAnnotations;
using System.Reflection;
using PocketSmith.NET.Models;

namespace PocketSmith.NET.Extensions;

public static class EnumExtensions
{
    public static string GetDisplayName(this Enum enumValue)

    {
        var members = enumValue.GetType().GetMember(enumValue.ToString());

        if (members == null || members.Length == 0)
        {
            throw new InvalidOperationException("Cannot get enumerator members.");
        }

        var attributes = members[0].GetCustomAttributes(typeof(DisplayAttribute), false);

        if (attributes == null || !attributes.Any())
        {
            throw new NullReferenceException(
                "The specified enumerator does not contain any properties with the [Display()] attribute.");
        }

        return ((DisplayAttribute)attributes.ElementAt(0)).Name!;
    }

    public static bool TryParse(string inputString, out PocketSmithAccountType? accountType)
    {
        var members = typeof(PocketSmithAccountType).GetMembers();

        foreach (var member in members)
        {
            var selectedAttribute = member.GetCustomAttribute(typeof(DisplayAttribute));
            if (selectedAttribute == null)
            {
                continue;
            }

            string? attributeValue = ((DisplayAttribute)selectedAttribute!).Name;

            if (attributeValue == inputString)
            {
                var success = Enum.TryParse(member.Name, true, out PocketSmithAccountType result);
                if (success)
                {
                    accountType = result;
                    return true;
                }
            }

        }
        accountType = null;
        return false;
    }

    public static bool TryParse(string inputString, out PocketSmithRefundBehavior? accountType)
    {
        var members = typeof(PocketSmithRefundBehavior).GetMembers();

        foreach (var member in members)
        {
            var selectedAttribute = member.GetCustomAttribute(typeof(DisplayAttribute));
            if (selectedAttribute == null)
            {
                continue;
            }

            string attributeValue = ((DisplayAttribute)selectedAttribute!).Name;

            if (attributeValue == inputString)
            {
                var success = Enum.TryParse(member.Name, true, out PocketSmithRefundBehavior result);
                if (success)
                {
                    accountType = result;
                    return true;
                }
            }

        }
        accountType = null;
        return false;
    }

    public static bool TryParse(string inputString, out PocketSmithTransactionType? accountType)
    {
        var members = typeof(PocketSmithTransactionType).GetMembers();

        foreach (var member in members)
        {
            var selectedAttribute = member.GetCustomAttribute(typeof(DisplayAttribute));
            if (selectedAttribute == null)
            {
                continue;
            }

            string attributeValue = ((DisplayAttribute)selectedAttribute!).Name;

            if (attributeValue == inputString)
            {
                var success = Enum.TryParse(member.Name, true, out PocketSmithTransactionType result);
                if (success)
                {
                    accountType = result;
                    return true;
                }
            }
        }
        accountType = null;
        return false;
    }
}