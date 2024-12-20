﻿using Phoenix.DataHandle.Utilities;
using Phoenix.Language.Types.RoleRank;

namespace Phoenix.DataHandle.Main.Types
{
    public enum RoleRank
    {
        // Inactive roles
        None = 0,
        
        // Client roles
        Student = RoleHierarchy.ClientRolesBase,
        Parent,
        
        // Staff roles
        Teacher = RoleHierarchy.StaffRolesBase,
        Secretary,
        SchoolAdmin,
        SchoolOwner,

        // Backend roles
        SchoolTester = RoleHierarchy.BackendRolesBase,  // School backend role
        SchoolDeveloper,
        SuperTester = RoleHierarchy.SuperRolesBase,     // Super backend roles
        SuperAdmin
    }

    public static class RoleHierarchy
    {
        public const int ClientRolesBase = 10;
        public const int StaffRolesBase = 20;
        public const int BackendRolesBase = 30;
        public const int SuperRolesBase = 35;
    }

    public static class RoleExtensions
    {
        public static bool IsClient(this RoleRank me) => 
            (int)me >= RoleHierarchy.ClientRolesBase && (int)me < RoleHierarchy.StaffRolesBase;
        public static bool IsStaff(this RoleRank me) => 
            (int)me >= RoleHierarchy.StaffRolesBase && (int)me < RoleHierarchy.BackendRolesBase;
        public static bool IsBackend(this RoleRank me) => (int)me >= RoleHierarchy.BackendRolesBase;
        public static bool IsSuper(this RoleRank me) => (int)me >= RoleHierarchy.SuperRolesBase;
        public static bool IsSchoolBackend(this RoleRank me) => me.IsBackend() && !me.IsSuper();
        public static bool IsStaffHead(this RoleRank me) => me.IsStaff() && me >= RoleRank.SchoolAdmin;
        public static bool IsStaffOrBackend(this RoleRank me) => me.IsStaff() || me.IsBackend();
        public static bool IsActive(this RoleRank me) => me != RoleRank.None;

        public static RoleRank[] AllRoleRanks => Enum.GetValues<RoleRank>();
        public static RoleRank[] ClientRoleRanks => AllRoleRanks.Where(rr => rr.IsClient()).ToArray();
        public static RoleRank[] StaffRoleRanks => AllRoleRanks.Where(rr => rr.IsStaff()).ToArray();
        public static RoleRank[] BackendRoleRanks => AllRoleRanks.Where(rr => rr.IsBackend()).ToArray();
        public static RoleRank[] SuperRoleRanks => AllRoleRanks.Where(rr => rr.IsSuper()).ToArray();
        public static RoleRank[] SchoolBackendRoleRanks => AllRoleRanks.Where(rr => rr.IsSchoolBackend()).ToArray();
        public static RoleRank[] StaffHeadRoleRanks => AllRoleRanks.Where(rr => rr.IsStaffHead()).ToArray();
        public static RoleRank[] StaffAndBackendRoleRanks => AllRoleRanks.Where(rr => rr.IsStaffOrBackend()).ToArray();
        public static RoleRank[] ActiveRoleRanks => AllRoleRanks.Where(rr => rr.IsActive()).ToArray();

        public static string ToNormalizedString(this RoleRank me)
        {
            return me switch
            {
                RoleRank.SchoolAdmin        => "SCHOOL_ADMIN",
                RoleRank.SchoolOwner        => "SCHOOL_OWNER",
                RoleRank.SchoolTester       => "SCHOOL_TESTER",
                RoleRank.SchoolDeveloper    => "SCHOOL_DEV",
                RoleRank.SuperTester        => "SUPER_TESTER",
                RoleRank.SuperAdmin         => "SUPER_ADMIN",
                _                           => me.ToString().ToUpper()
            };
        }

        public static string ToFriendlyString(this RoleRank me)
        {
            return me switch
            {
                RoleRank.Student            => RoleRankResources.Student,
                RoleRank.Parent             => RoleRankResources.Parent,
                RoleRank.Teacher            => RoleRankResources.Teacher,
                RoleRank.Secretary          => RoleRankResources.Secretary,
                RoleRank.SchoolAdmin        => RoleRankResources.SchoolAdmin,
                RoleRank.SchoolOwner        => RoleRankResources.SchoolOwner,
                RoleRank.SchoolTester       => RoleRankResources.SchoolTester,
                RoleRank.SchoolDeveloper    => RoleRankResources.SchoolDeveloper,
                RoleRank.SuperTester        => RoleRankResources.SuperTester,
                RoleRank.SuperAdmin         => RoleRankResources.SuperAdmin,
                _                           => string.Empty
            };
        }

        public static string[] GetFriendlyStrings()
        {
            return Enum.GetValues<RoleRank>()
                .Select(v => v.ToFriendlyString())
                .Where(s => !string.IsNullOrWhiteSpace(s))
                .ToArray();
        }

        private static bool RoleRankPredicate(RoleRank rr, string str) =>
            rr.ToString().Equals(str, StringComparison.OrdinalIgnoreCase) ||
            rr.ToFriendlyString().ToUnaccented().Equals(str.ToUnaccented(), StringComparison.OrdinalIgnoreCase) ||
            rr.ToNormalizedString().Equals(str, StringComparison.OrdinalIgnoreCase);

        public static RoleRank ToRoleRank(this string me)
        {
            return AllRoleRanks.SingleOrDefault(rr => RoleRankPredicate(rr, me.Replace("_", "")));
        }

        public static bool TryToRoleRank(this string me, out RoleRank roleRank)
        {
            roleRank = me.ToRoleRank();

            return AllRoleRanks.Any(rr => RoleRankPredicate(rr, me.Replace("_", "")));
        }
    }
}
