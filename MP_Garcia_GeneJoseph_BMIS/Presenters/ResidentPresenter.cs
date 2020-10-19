﻿using MP_Garcia_GeneJoseph_BMIS.Helpers;
using MP_Garcia_GeneJoseph_BMIS.Models;
using MP_Garcia_GeneJoseph_BMIS.Views;
using MP_Garcia_GeneJoseph_BMIS.Views.ResidentView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MP_Garcia_GeneJoseph_BMIS.Presenters
{
    class ResidentPresenter
    {
        private Entities dbEnt = new Entities();
        public void GetAddResident()
        {
            ViewContext.Dispose();

            ViewResidentView view = new ViewResidentView();

            ViewContext.ActiveForm = view;
            ViewContext.ActiveForm.ShowDialog();
        }

        /// <param name="newResident">Contains the complete information of the new resident, except for an residentId/param>
        public void PostAddResident(IResident newResident)
        {
            newResident.Resident.ResidentId = dbEnt.Resident.Residents().Max(m => m.ResidentId) + 1;

            bool status = dbEnt.Resident.InsertResident(newResident.Resident);

            if (status)
            {
                MessageBox.Show("Resident " + newResident.Resident.FirstName + " " + newResident.Resident.LastName + " was successfully added.", "New Resident Record", MessageBoxButtons.OK, MessageBoxIcon.Information);
                // go back to landing page
                /* Audit TRAIL RECORD and System PROMPT */
                MenuHelper.MenuInput();

            }
            else
            {
                MessageBox.Show("Resident was not added.", "New Resident Record", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // reload view
                new ResidentPresenter().GetAddResident();
            }
        }

        /// <summary>
        /// basically renders the view where the user a resident by its name
        /// </summary>
        public void GetSearchResident()
        {
            // render view
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="search">Contains the search parameters to find a resident record</param>
        public void PostSearchResident(IResident searchView)
        {
            Resident searched = dbEnt.Resident.Residents().
                Where(m => 
                m.FirstName.ToUpper() == searchView.Resident.FirstName.ToUpper() &&
                m.MiddleName.ToUpper() == searchView.Resident.MiddleName.ToUpper() &&
                m.LastName.ToUpper() == searchView.Resident.LastName.ToUpper())
                .FirstOrDefault();

            if (searched != null)
            {
                new ResidentPresenter().GetViewResident(searched.ResidentId);
            }
            else
            {
                MessageBox.Show("Resident cannot be found", "View Resident", MessageBoxButtons.OK, MessageBoxIcon.Error);
                new ResidentPresenter().GetDisplayResidents();
            }
        }

        /// <summary>
        /// The view dispays an action button to trigger GetViewResident(id), and PostToResidentDeceased(id)
        /// </summary>
        public void GetDisplayResidents()
        {
            ViewContext.Dispose();

            DisplayResidentsView view = new DisplayResidentsView();
            view.Residents = dbEnt.Resident.Residents().OrderBy(m=>m.Status).ToList();
            view.PopulateDataList();
            ViewContext.ActiveForm = view;
            ViewContext.ActiveForm.ShowDialog();
        }

        /// <summary>
        /// Displays a specific resident, the view allows the fields to be edited, including the status
        /// </summary>
        public void GetViewResident(int residentId)
        {
            Resident resident = dbEnt.Resident.Residents().Where(m => m.ResidentId == residentId).FirstOrDefault();

            if (resident != null)
            {
                // render view
            }
            else
            {
                MessageBox.Show("Resident cannot be found", "View Resident", MessageBoxButtons.OK, MessageBoxIcon.Error);
                new ResidentPresenter().GetDisplayResidents();
            }

        }

        /// <param name="view">Contains the updated resident model, and the list of currenet residents</param>
        public void PostUpdateResident(IResident view)
        {
            // remove the old version resident from residents
            view.Residents.Remove(view.Residents.Where(m => m.ResidentId == view.Resident.ResidentId).FirstOrDefault());
            // re-insert the new version of resident to list
            view.Residents.Add(view.Resident);
            // update text file

            bool status = dbEnt.Resident.SaveResidents(view.Residents);

            if (status)
            {
                MessageBox.Show("Resident " + view.Resident.FirstName + " " + view.Resident.LastName + "'s record was updated successfully.", "Update Resident", MessageBoxButtons.OK, MessageBoxIcon.Information);
                // go back to landing page
                /* Audit TRAIL RECORD and System PROMPT */
                MenuHelper.MenuInput();

            }
            else
            {
                MessageBox.Show("Resident " + view.Resident.FirstName + " " + view.Resident.LastName + "'s record was not updated.", "Update Resident", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // reload view
                new ResidentPresenter().GetDisplayResidents();
            }
        }

        public void PostToResidentDeceased(int residentId)
        {
            List<Resident> residents = dbEnt.Resident.Residents();
            Resident toDeceased = residents.Where(m => m.ResidentId == residentId).FirstOrDefault();

            if (toDeceased != null)
            {
                toDeceased.Status = SystemConstants.RESIDENT_STATUS_DECEASED;
                // remove from list
                residents.Remove(toDeceased);
                // re-insert
                residents.Add(toDeceased);
                // update text file
                bool status = dbEnt.Resident.SaveResidents(residents);

                if (status)
                {
                    MessageBox.Show("Resident " + toDeceased.FirstName + " " + toDeceased.LastName + "'s status is set to as deceased.", "Deceased Resident", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    // go back to landing page
                    /* Audit TRAIL RECORD and System PROMPT */
                    MenuHelper.MenuInput();

                }
                else
                {
                    MessageBox.Show("Resident " + toDeceased.FirstName + " " + toDeceased.LastName + "'s status was not able to be set to deceased.", "Deceased Resident", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    // reload view
                    new ResidentPresenter().GetViewResident(toDeceased.ResidentId);
                }
            }
            else
            {
                MessageBox.Show("Resident cannot be found.", "Deceased Resident", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // reload view
                new ResidentPresenter().GetDisplayResidents();
            }
        }

        public void GetDisplayFamilies()
        {
            List<Family> families = dbEnt.Family.Families();
            // render view
        }

        public void GetAddFamily()
        {
            List<Family> existingFamilies = dbEnt.Family.Families();
            // obtain the resident Id of those that already have family records
            List<int> idsP1 = existingFamilies.Select(m => m.ParentOneId).ToList();
            List<int> idsP2 = existingFamilies.Select(m => m.ParentTwoId).ToList();

            // filters the residents, does not display residents that already have family records
            // ! Issue - duplicate residents choice on both data tables
            List<Resident> parentChoices = dbEnt.Resident.Residents().Where(m=> !idsP1.Contains(m.ResidentId) && !idsP2.Contains(m.ResidentId)).ToList();

            // RENDER VIEW
            // the view will still inherit IResident
        }

        /// <param name="parentOneId"></param>
        /// <param name="parentTwoId">Optional</param>
        /// <param name="familyMembers"></param>
        public void PostSaveFamily(int parentOneId, int parentTwoId, int familyMembers)
        {
            Family newFamily = new Family();
            newFamily.FamilyId = dbEnt.Family.Families().Max(m=>m.FamilyId) + 1;
            newFamily.FamilyMembers = familyMembers;
            newFamily.ParentOneId = parentOneId;

            if (parentTwoId != null || parentTwoId > 0)
                newFamily.ParentTwoId = parentTwoId;

            bool status = dbEnt.Family.InsertFamily(newFamily);

            if (status)
            {
                MessageBox.Show("Family record was successfully created.", "New Family Record", MessageBoxButtons.OK, MessageBoxIcon.Information);
                // go back to landing page
                /* Audit TRAIL RECORD and System PROMPT */
                MenuHelper.MenuInput();

            }
            else
            {
                MessageBox.Show("Unavailable to create new family record.", "New Family Record", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // reload view
                new ResidentPresenter().GetAddFamily();
            }
        }
    }
}
