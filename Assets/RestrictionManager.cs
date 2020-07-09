using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestrictionManager : ScriptableObject
{
    private RestrictionListGroup[] CreateRestrictionLists(RestrictionListGroup[] restrictionListGroups, string path)
    {
        RestrictionListGroup[] backup = restrictionListGroups;

        ListManager[] ListManagers;

        // Get all Restriction Lists
        ListManagers = Resources.LoadAll<ListManager>("Restriction Lists/" + path);

        // Set array to same number of list groups
        restrictionListGroups = new RestrictionListGroup[ListManagers.Length];

        for (int i = 0; i < restrictionListGroups.Length; i++)
        {
            restrictionListGroups[i] = new RestrictionListGroup();
            restrictionListGroups[i].restrictionLists = new RestrictionList[ListManagers[i].objectLists.Length];
            restrictionListGroups[i].name = ListManagers[i].name;

            for (int j = 0; j < restrictionListGroups[i].restrictionLists.Length; j++)
            {
                restrictionListGroups[i].restrictionLists[j] = new RestrictionList();
                restrictionListGroups[i].restrictionLists[j].restrictions = new Restriction[ListManagers[i].objectLists[j].objects.Length];
                restrictionListGroups[i].restrictionLists[j].name = ListManagers[i].objectLists[j].name;

                for (int k = 0; k < restrictionListGroups[i].restrictionLists[j].restrictions.Length; k++)
                {
                    restrictionListGroups[i].restrictionLists[j].restrictions[k] = new Restriction();
                    restrictionListGroups[i].restrictionLists[j].restrictions[k].name = ListManagers[i].objectLists[j].objects[k].name;

                    if (i < backup.Length && j < backup[i].restrictionLists.Length && k < backup[i].restrictionLists[j].restrictions.Length)
                    {
                        restrictionListGroups[i].restrictionLists[j].restrictions[k].restriction = backup[i].restrictionLists[j].restrictions[k].restriction;
                    }
                }
            }
        }

        return restrictionListGroups;
    }

    public RestrictionListGroup CreateRestrictionLists(RestrictionListGroup restrictionListGroup, string path)
    {
        RestrictionListGroup[] restrictionListGroups = new RestrictionListGroup[1];
        restrictionListGroups[0] = restrictionListGroup;

        restrictionListGroups = CreateRestrictionLists(restrictionListGroups);

        return restrictionListGroups[0];
    }

    public RestrictionListGroup[] CreateRestrictionLists(RestrictionListGroup[] restrictionListGroups)
    {
        restrictionListGroups = CreateRestrictionLists(restrictionListGroups, "");

        return restrictionListGroups;
    }
}
