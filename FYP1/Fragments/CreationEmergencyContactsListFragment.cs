using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using FYP.Business.Models;
using FYP_Droid.Utilities;
using System.Collections.Generic;
using System.Linq;
using SupprtToolbar = Android.Support.V7.Widget.Toolbar;

namespace FYP_Droid.Fragments
{
    public class CreationEmergencyContactsListFragment : BaseFragment
    {

        #region Fields

        private ListView m_contactsListView;

        private List<EmergencyContact> m_contactsOnPhone;
        private List<EmergencyContact> m_contactsToSave;

        private Button m_updateContactListBtn;
        private CheckBox m_selectDeselectAllChkBox;
        private bool m_inEditMode;

        #endregion //Fields

        #region Property Accessors

        /// <summary>
        /// Gets/Sets m_contactsListView
        /// </summary>
        public ListView ContactsListView
        {
            get
            {
                return m_contactsListView;
            }

            set
            {
                m_contactsListView = value;
            }
        }

        /// <summary>
        /// Gets/Sets m_updateContactListBtn
        /// </summary>
        public Button UpdateContactListBtn
        {
            get
            {
                return m_updateContactListBtn;
            }

            set
            {
                m_updateContactListBtn = value;
            }
        }

        /// <summary>
        /// Gets/Sets m_contactsOnPhone
        /// </summary>
        public List<EmergencyContact> ContactsOnPhone
        {
            get
            {
                return m_contactsOnPhone;
            }

            set
            {
                m_contactsOnPhone = value;
            }
        }

        /// <summary>
        /// Gets/Sets m_selectDeselectAllChkBox
        /// </summary>
        public CheckBox SelectDeselectAllChkBox
        {
            get
            {
                return m_selectDeselectAllChkBox;
            }

            set
            {
                m_selectDeselectAllChkBox = value;
            }
        }

        /// <summary>
        /// Gets/Sets m_contactsToSave
        /// </summary>
        public List<EmergencyContact> ContactsToSave
        {
            get
            {
                return m_contactsToSave;
            }

            set
            {
                m_contactsToSave = value;
            }
        }

        /// <summary>
        /// Gets/Sets m_inEditMode
        /// </summary>
        public bool InEditMode
        {
            get
            {
                return m_inEditMode;
            }

            set
            {
                m_inEditMode = value;
            }
        }

        #endregion //Property Accessors

        #region Methods

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            //inflate the fragments view
            InflateView(inflater, container, Resource.Layout.CreationEmergencyContactsListView);
            //setup UI
            SetupUIVariables();
            //setup utility variables
            SetupUtilityVariables();
            //handle events
            EventHandlers();
            //retur view
            return FragView;
        }

        /// <summary>
        /// Setting up UI variabes for sue within this class
        /// </summary>
        private void SetupUIVariables()
        {
            Toolbar = Activity.FindViewById<SupprtToolbar>(Resource.Id.toolbar);
            Toolbar.Title = "My Emergency Contacts";
            ContactsListView = FragView.FindViewById<ListView>(Resource.Id.ContactsListView);
            SelectDeselectAllChkBox = FragView.FindViewById<CheckBox>(Resource.Id.selectDeselectAllContacts);
            UpdateContactListBtn = FragView.FindViewById<Button>(Resource.Id.UpdateEmergencyContactListBtn);
        }

        /// <summary>
        /// Sets up utility variables for use within this class
        /// </summary>
        private void SetupUtilityVariables()
        {
            //setting contacts adapter
            var contactsAdapter = new ContactsAdapter(Activity);
            //getting contacts on phoe
            ContactsOnPhone = contactsAdapter.FillContacts();
            //setting list adapter
            ContactsListView.Adapter = new ArrayAdapter(Activity.ApplicationContext, Android.Resource.Layout.SimpleListItemMultipleChoice, ContactsOnPhone.ToArray());
            //setting background clour
            ContactsListView.SetBackgroundColor(Android.Graphics.Color.DarkGray);
            //setting choice mode
            ContactsListView.ChoiceMode = ChoiceMode.Multiple;
            //new list of contacts to save
            ContactsToSave = new List<EmergencyContact>();
            //itterate contacts and check saved contacts
            foreach (var contact in ContactsOnPhone)
            {
                var contactCOunt = GlobalUtilities.EmergencyContactManager.GetAllEmergencyContacts().Where(x => x.ContactNumber == contact.ContactNumber).Count();

                if (contactCOunt > 0)
                {
                    ContactsListView.SetItemChecked(ContactsOnPhone.IndexOf(contact), true);
                }
                else
                {
                    ContactsListView.SetItemChecked(ContactsOnPhone.IndexOf(contact), false);
                }
            }
        }

        /// <summary>
        /// Handle Events
        /// </summary>
        private void EventHandlers()
        {
            //handle contact selected
            ContactsListView.ItemSelected += (s, e) =>
            {
                //creating new emmergency contact
                EmergencyContact contact = ContactsOnPhone.ElementAtOrDefault(e.Position);

                //if checked
                if (ContactsListView.IsItemChecked(e.Position))
                {
                    //uncheck
                    ContactsListView.SetItemChecked(e.Position, false);
                    //if list of contacts to save contains this contact
                    if (ContactsToSave.Contains<EmergencyContact>(contact))
                    {
                        //remove
                        ContactsToSave.Remove(contact);
                    }
                }
                else
                {
                    //check item
                    ContactsListView.SetItemChecked(e.Position, true);
                    //if the list of contacts to save does not already contain contact
                    if (!ContactsToSave.Contains<EmergencyContact>(contact))
                    {
                        //add contact
                        ContactsToSave.Add(contact);
                    }
                }
            };

            //handle update contacts button
            UpdateContactListBtn.Click += (s, e) =>
            {
                //delete current contacts
                GlobalUtilities.EmergencyContactManager.DeleteAllEmergencyContacts();
                //new spare item array for checcked item positions
                var sparseArray = ContactsListView.CheckedItemPositions;
                //itterate sparse array
                for (var i = 0; i < sparseArray.Size(); i++)
                {
                    //if array has value at i
                    if (sparseArray.ValueAt(i) == true)
                    {
                        //set contact to save
                        var contactToSave = ContactsOnPhone.ElementAtOrDefault(i);
                        //save emergency contact
                        GlobalUtilities.EmergencyContactManager.SaveEmergencyContact(contactToSave);
                    }
                }
                //toast to user
                Toast.MakeText(Activity.ApplicationContext, "Updated Emergency Contacts", ToastLength.Short).Show();
                //load dashboard
                LoadFragment(new DashoardSummaryFragment());
            };

            //select all box changed
            SelectDeselectAllChkBox.CheckedChange += (s, e) =>
            {
                //num of items checked
                var checkedContactsCount = ContactsListView.CheckedItemCount;
                //if atleast one
                if (checkedContactsCount > 0)
                {
                    //iterate over contcts on phone
                    foreach (var contact in ContactsOnPhone)
                    {
                        //deselect
                        ContactsListView.SetItemChecked(ContactsOnPhone.IndexOf(contact), false);
                    }
                }
                else
                {
                    //iterate over contcts on phone
                    foreach (var contact in ContactsOnPhone)
                    {
                        //select
                        ContactsListView.SetItemChecked(ContactsOnPhone.IndexOf(contact), true);
                    }
                }
            };
        }

        #endregion //Methods

    }
}