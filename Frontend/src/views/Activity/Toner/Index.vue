<template>
  <admin-layout>
    <PageBreadcrumb :pageTitle="currentPageTitle" />

  <div class="grid grid-cols-1 gap-4 sm:grid-cols-2 lg:grid-cols-3 xl:grid-cols-5 md:gap-6">

    <div class="rounded-2xl border border-gray-200 bg-white p-5 dark:border-gray-800 dark:bg-white/[0.03] md:p-6">
      <router-link to="/setup/printer">
      <div class="flex items-end justify-between">
        <div>
      
          <h5 class="mt-2 font-bold text-gray-800 text-title-sm dark:text-white/90 text-lg">Backup Toner for Customer</h5>
        </div>
      </div>
      </router-link>
    </div>

    <div class="rounded-2xl border border-gray-200 bg-white p-5 dark:border-gray-800 dark:bg-white/[0.03] md:p-6">
      <router-link to="/setup/toner">
      <div class="flex items-end justify-between">
        <div>
      
          <h5 class="mt-2 font-bold text-gray-800 text-title-sm dark:text-white/90 text-lg">Return Toner</h5>
        </div>
      </div>
      </router-link>
    </div>

    <div class="rounded-2xl border border-gray-200 bg-white p-5 dark:border-gray-800 dark:bg-white/[0.03] md:p-6">
      <router-link to="/setup/toner">
      <div class="flex items-end justify-between">
        <div>
      
          <h5 class="mt-2 font-bold text-gray-800 text-title-sm dark:text-white/90 text-lg">Replace Toner</h5>
        </div>
      </div>
      </router-link>
    </div>

    <div class="rounded-2xl border border-gray-200 bg-white p-5 dark:border-gray-800 dark:bg-white/[0.03] md:p-6">
      <router-link to="/setup/toner">
      <div class="flex items-end justify-between">
        <div>
      
          <h5 class="mt-2 font-bold text-gray-800 text-title-sm dark:text-white/90 text-lg">Service Toner on Customer</h5>
        </div>
      </div>
      </router-link>
    </div>
    
  </div>
  </admin-layout>
</template>

<script setup>
import AdminLayout from '../../../components/layout/AdminLayout.vue'
import PageBreadcrumb from '@/components/common/PageBreadcrumb.vue'
import { ref } from 'vue'
import ProfileCard from '../../../components/profile/ProfileCard.vue'
import PersonalInfoCard from '../../../components/profile/PersonalInfoCard.vue'
import AddressCard from '../../../components/profile/AddressCard.vue'
const currentPageTitle = ref('Activity Toner')
</script>
