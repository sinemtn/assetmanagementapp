import { createRouter, createWebHistory } from 'vue-router'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  scrollBehavior(to, from, savedPosition) {
    return savedPosition || { left: 0, top: 0 }
  },
  routes: [
  
    {
      path: '/',
      name: 'Dashboard',
      component: () => import('../views/Dashboard.vue'),
      meta: {
        title: 'Dashboard',
      },
    },

    {
      path: '/master-data',
      name: 'Master Data',
      component: () => import('../views/Master/Index.vue'),
      meta: {
        title: 'Master Data',
      },
    },

    {
      path: '/master-data/customer',
      name: 'Master Data Customer',
      component: () => import('../views/Master/Customer.vue'),
      meta: {
        title: 'Master Data Customer',
      },
    },
    
    {
      path: '/master-data/customer/tambah',
      name: 'Tambah Data Customer',
      component: () => import('../views/Master/TambahCustomer.vue'),
      meta: {
        title: 'Tambah Data Customer',
      },
    },

    {
      path: '/master-data/printer',
      name: 'Master Data Printer',
      component: () => import('../views/Master/Printer.vue'),
      meta: {
        title: 'Master Data Printer',
      },
    },

    {
      path: '/stock',
      name: 'Stock',
      component: () => import('../views/Stock/Index.vue'),
      meta: {
        title: 'Stock',
      },
    },
    {
      path: '/stock/printer',
      name: 'Stock Printer',
      component: () => import('../views/Stock/Printer.vue'),
      meta: {
        title: 'Stock Printer',
      },
    },
    {
      path: '/stock/printer/tambah',
      name: 'Tambah Printer',
      component: () => import('../views/Stock/TambahPrinter.vue'),
      meta: {
        title: 'Stock Printer',
      },
    },
    {
      path: '/stock/printer/edit',
      name: 'Edit Printer',
      component: () => import('../views/Stock/EditPrinter.vue'),
      meta: {
        title: 'Stock Printer',
      },
    },


    {
      path: '/stock/toner',
      name: 'Toner Printer',
      component: () => import('../views/Stock/Toner.vue'),
      meta: {
        title: 'Stock Toner',
      },
    },
    {
      path: '/stock/sparepart',
      name: 'Toner Sparepart',
      component: () => import('../views/Stock/Sparepart.vue'),
      meta: {
        title: 'Sparepart Toner',
      },
    },

    {
      path: '/surattugas',
      name: 'Surat Tugas',
      component: () => import('../views/SuratTugas/Index.vue'),
      meta: {
        title: 'Surat Tugas',
      },
    },

    {
      path: '/surattugas-detail',
      name: 'Surat Tugas Detail',
      component: () => import('../views/SuratTugas/Detail.vue'),
      meta: {
        title: 'Surat Tugas Detail',
      },
    },   
    
    {
      path: '/surattugas/tambah',
      name: 'Tambah Surat Tugas',
      component: () => import('../views/SuratTugas/Tambah.vue'),
      meta: {
        title: 'Tambah Surat Tugas',
      },
    }, 


    {
      path: '/activity-printer',
      name: 'Activity Printer',
      component: () => import('../views/Activity/Printer/Index.vue'),
      meta: {
        title: 'Activity Printer',
      },
    },


    {
      path: '/activity-sparepart',
      name: 'Activity Sparepart',
      component: () => import('../views/Activity/Sparepart/Index.vue'),
      meta: {
        title: 'Activity Sparepart',
      },
    },


    {
      path: '/activity-toner',
      name: 'Activity Toner',
      component: () => import('../views/Activity/Toner/Index.vue'),
      meta: {
        title: 'Activity Toner',
      },
    },

    {
      path: '/complaint',
      name: 'Complaint',
      component: () => import('../views/Complaint/Index.vue'),
      meta: {
        title: 'Complaint',
      },
    },

    {
      path: '/complaint/detail',
      name: 'Komplain Detail',
      component: () => import('../views/Complaint/Detail.vue'),
      meta: {
        title: 'Komplain Detil',
      },
    },


    {
      path: '/setup',
      name: 'Setup',
      component: () => import('../views/Setup/Index.vue'),
      meta: {
        title: 'Setup',
      },
    },
    {
      path: '/setup/printer',
      name: 'Setup Printer',
      component: () => import('../views/Setup/Printer.vue'),
      meta: {
        title: 'Setup Printer',
      },
    },
    {
      path: '/setup/customer',
      name: 'Setup Customer',
      component: () => import('../views/Setup/Customer.vue'),
      meta: {
        title: 'Setup Customer',
      },
    },
    {
      path: '/setup/sparepart',
      name: 'Setup Sparepart',
      component: () => import('../views/Setup/Sparepart.vue'),
      meta: {
        title: 'Setup Sparepart',
      },
    },
    {
      path: '/setup/toner',
      name: 'Setup Toner',
      component: () => import('../views/Setup/Toner.vue'),
      meta: {
        title: 'Setup Toner',
      },
    },
    {
      path: '/setup/user',
      name: 'Setup User',
      component: () => import('../views/Setup/User.vue'),
      meta: {
        title: 'Setup User',
      },
    },
    {
      path: '/calendar',
      name: 'Calendar',
      component: () => import('../views/Others/Calendar.vue'),
      meta: {
        title: 'Calendar',
      },
    },
    {
      path: '/profile',
      name: 'Profile',
      component: () => import('../views/Others/UserProfile.vue'),
      meta: {
        title: 'Profile',
      },
    },
    
    {
      path: '/form-elements',
      name: 'Form Elements',
      component: () => import('../views/Forms/FormElements.vue'),
      meta: {
        title: 'Form Elements',
      },
    },
    {
      path: '/users',
      name: 'Users',
      component: () => import('../views/Users/Index.vue'),
      meta: {
        title: 'Users',
      },
    },
    {
      path: '/line-chart',
      name: 'Line Chart',
      component: () => import('../views/Chart/LineChart/LineChart.vue'),
    },
    {
      path: '/bar-chart',
      name: 'Bar Chart',
      component: () => import('../views/Chart/BarChart/BarChart.vue'),
    },
    {
      path: '/alerts',
      name: 'Alerts',
      component: () => import('../views/UiElements/Alerts.vue'),
      meta: {
        title: 'Alerts',
      },
    },
    {
      path: '/avatars',
      name: 'Avatars',
      component: () => import('../views/UiElements/Avatars.vue'),
      meta: {
        title: 'Avatars',
      },
    },
    {
      path: '/badge',
      name: 'Badge',
      component: () => import('../views/UiElements/Badges.vue'),
      meta: {
        title: 'Badge',
      },
    },

    {
      path: '/buttons',
      name: 'Buttons',
      component: () => import('../views/UiElements/Buttons.vue'),
      meta: {
        title: 'Buttons',
      },
    },

    {
      path: '/images',
      name: 'Images',
      component: () => import('../views/UiElements/Images.vue'),
      meta: {
        title: 'Images',
      },
    },
    {
      path: '/videos',
      name: 'Videos',
      component: () => import('../views/UiElements/Videos.vue'),
      meta: {
        title: 'Videos',
      },
    },
    {
      path: '/blank',
      name: 'Blank',
      component: () => import('../views/Pages/BlankPage.vue'),
      meta: {
        title: 'Blank',
      },
    },

    {
      path: '/error-404',
      name: '404 Error',
      component: () => import('../views/Errors/FourZeroFour.vue'),
      meta: {
        title: '404 Error',
      },
    },

    {
      path: '/signin',
      name: 'Signin',
      component: () => import('../views/Auth/Signin.vue'),
      meta: {
        title: 'Signin',
      },
    },
    {
      path: '/signup',
      name: 'Signup',
      component: () => import('../views/Auth/Signup.vue'),
      meta: {
        title: 'Signup',
      },
    },
    // AUDIT TRAIL 
    {
      path: '/audittrail',
      name: 'Audit Trail',
      component: () => import('../views/AuditTrail/Index.vue'),
      meta: {
        title: 'Audit Trail',
      },
    },

  ],
})

export default router

router.beforeEach((to, from, next) => {
  document.title = `Multiprint ${to.meta.title} `
  next()
})
