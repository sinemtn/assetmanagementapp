<template>

  <div>

    <div class="flex justify-between items-center">
      <form class="mr-2">
        <div class="relative">
          <button class="absolute -translate-y-1/2 left-4 top-1/2">
            <svg class="fill-gray-500 dark:fill-gray-400" width="20" height="20" viewBox="0 0 20 20" fill="none"
              xmlns="http://www.w3.org/2000/svg">
              <path fill-rule="evenodd" clip-rule="evenodd"
                d="M3.04175 9.37363C3.04175 5.87693 5.87711 3.04199 9.37508 3.04199C12.8731 3.04199 15.7084 5.87693 15.7084 9.37363C15.7084 12.8703 12.8731 15.7053 9.37508 15.7053C5.87711 15.7053 3.04175 12.8703 3.04175 9.37363ZM9.37508 1.54199C5.04902 1.54199 1.54175 5.04817 1.54175 9.37363C1.54175 13.6991 5.04902 17.2053 9.37508 17.2053C11.2674 17.2053 13.003 16.5344 14.357 15.4176L17.177 18.238C17.4699 18.5309 17.9448 18.5309 18.2377 18.238C18.5306 17.9451 18.5306 17.4703 18.2377 17.1774L15.418 14.3573C16.5365 13.0033 17.2084 11.2669 17.2084 9.37363C17.2084 5.04817 13.7011 1.54199 9.37508 1.54199Z"
                fill="" />
            </svg>
          </button>
          <input type="text" placeholder="Search ..."
            class="dark:bg-dark-900 h-11 w-full rounded-lg border border-gray-200 bg-transparent py-2.5 pl-12 pr-14 text-sm text-gray-800 shadow-theme-xs placeholder:text-gray-400 focus:border-brand-300 focus:outline-hidden focus:ring-3 focus:ring-brand-500/10 dark:border-gray-800 dark:bg-gray-900 dark:bg-white/[0.03] dark:text-white/90 dark:placeholder:text-white/30 dark:focus:border-brand-800 xl:w-[280px]" />

          <button
            class="absolute right-2.5 top-1/2 inline-flex -translate-y-1/2 items-center gap-0.5 rounded-lg border border-gray-200 bg-gray-50 px-[7px] py-[4.5px] text-xs -tracking-[0.2px] text-gray-500 dark:border-gray-800 dark:bg-white/[0.03] dark:text-gray-400">
            <span> Search </span>

          </button>
        </div>
      </form>

      <!-- Filter Pencarian -->
      <div class="flex items-center cursor-pointer hover:text-brand-600 transition">
        <FlagIcon class="mr-1" />
        <button type="button" class="text-sm text-gray-700" @click="isAdvancedFilter = true">Filter Pencarian</button>
      </div>


      <Modal v-if="isAdvancedFilter" @close="isAdvancedFilter = false">
        <template #body>
          <div
            class="no-scrollbar relative w-full max-w-[700px] overflow-y-auto rounded-3xl bg-white p-4 dark:bg-gray-900 lg:p-11">
            <!-- close btn -->
            <button @click="isAdvancedFilter = false"
              class="transition-color absolute right-5 top-5 z-999 flex h-11 w-11 items-center justify-center rounded-full bg-gray-100 text-gray-400 hover:bg-gray-200 hover:text-gray-600 dark:bg-gray-700 dark:bg-white/[0.05] dark:text-gray-400 dark:hover:bg-white/[0.07] dark:hover:text-gray-300">
              <svg class="fill-current" width="24" height="24" viewBox="0 0 24 24" fill="none"
                xmlns="http://www.w3.org/2000/svg">
                <path fill-rule="evenodd" clip-rule="evenodd"
                  d="M6.04289 16.5418C5.65237 16.9323 5.65237 17.5655 6.04289 17.956C6.43342 18.3465 7.06658 18.3465 7.45711 17.956L11.9987 13.4144L16.5408 17.9565C16.9313 18.347 17.5645 18.347 17.955 17.9565C18.3455 17.566 18.3455 16.9328 17.955 16.5423L13.4129 12.0002L17.955 7.45808C18.3455 7.06756 18.3455 6.43439 17.955 6.04387C17.5645 5.65335 16.9313 5.65335 16.5408 6.04387L11.9987 10.586L7.45711 6.04439C7.06658 5.65386 6.43342 5.65386 6.04289 6.04439C5.65237 6.43491 5.65237 7.06808 6.04289 7.4586L10.5845 12.0002L6.04289 16.5418Z"
                  fill="" />
              </svg>
            </button>
            <div class="px-2 pr-14">
              <h4 class="mb-2 text-2xl font-semibold text-gray-800 dark:text-white/90">
                Filter Pencarian Komplain
              </h4>
            </div>
            <form class="flex flex-col">
              <div class="custom-scrollbar h-[458px] overflow-y-auto p-2">
                <div>
                  <div class="grid grid-cols-1 gap-x-6 gap-y-3 lg:grid-cols-2">

                    <div class="col-span-2">
                      <label class="mb-1.5 block text-sm font-medium text-gray-700 dark:text-gray-400">
                        Customer
                      </label>
                      <input type="text" value=""
                        class="dark:bg-dark-900 h-11 w-full appearance-none rounded-lg border border-gray-300 bg-transparent bg-none px-4 py-2.5 text-sm text-gray-800 shadow-theme-xs placeholder:text-gray-400 focus:border-brand-300 focus:outline-hidden focus:ring-3 focus:ring-brand-500/10 dark:border-gray-700 dark:bg-gray-900 dark:text-white/90 dark:placeholder:text-white/30 dark:focus:border-brand-800" />
                    </div>

                    <div class="col-span-2">
                      <label class="mb-1.5 block text-sm font-medium text-gray-700 dark:text-gray-400">
                        Status
                      </label>
                      <select
                        class="dark:bg-dark-900 h-11 w-full appearance-none rounded-lg border border-gray-300 bg-transparent px-4 py-2.5 text-sm text-gray-800 shadow-theme-xs focus:border-brand-300 focus:outline-hidden focus:ring-3 focus:ring-brand-500/10 dark:border-gray-700 dark:bg-gray-900 dark:text-white/90 dark:focus:border-brand-800">
                        <option value="">Solved</option>
                        <option value="">In Progress</option>
                      </select>
                    </div>
                  </div>

                  <div class="flex items-center gap-3 px-2 mt-6 lg:justify-end">
                    <button @click="isAdvancedFilter = false" type="button"
                      class="flex w-full justify-center rounded-lg border border-gray-300 bg-white px-4 py-2.5 text-sm font-medium text-gray-700 hover:bg-gray-50 dark:border-gray-700 dark:bg-gray-800 dark:text-gray-400 dark:hover:bg-white/[0.03] sm:w-auto">
                      Clear
                    </button>
                    <button @click="isAdvancedFilter" type="button"
                      class="flex w-full justify-center rounded-lg bg-brand-500 px-4 py-2.5 text-sm font-medium text-white hover:bg-brand-600 sm:w-auto">
                      Apply
                    </button>
                  </div>
                </div>
              </div>

            </form>
          </div>
        </template>
      </Modal>

      <div class="flex items-center ml-auto gap-2">
        <div
          class="rounded-xl border border-gray-200 bg-white p-3 dark:border-gray-800 dark:bg-white/[0.03] md:p-4 hover:shadow-sm transition">
          <router-link to="/master-data/customer/tambah" class="block">
            <span class="flex items-center gap-2">
              <PlusIcon />
              Tambah
            </span>
          </router-link>
        </div>

      </div>

    </div>
  </div>

  <Modal v-if="isProfileInfoModal" @close="isProfileInfoModal = false">
    <template #body>
      <div
        class="no-scrollbar relative w-full max-w-[700px] overflow-y-auto rounded-3xl bg-white p-4 dark:bg-gray-900 lg:p-11">
        <!-- close btn -->
        <button @click="isProfileInfoModal = false"
          class="transition-color absolute right-5 top-5 z-999 flex h-11 w-11 items-center justify-center rounded-full bg-gray-100 text-gray-400 hover:bg-gray-200 hover:text-gray-600 dark:bg-gray-700 dark:bg-white/[0.05] dark:text-gray-400 dark:hover:bg-white/[0.07] dark:hover:text-gray-300">
          <svg class="fill-current" width="24" height="24" viewBox="0 0 24 24" fill="none"
            xmlns="http://www.w3.org/2000/svg">
            <path fill-rule="evenodd" clip-rule="evenodd"
              d="M6.04289 16.5418C5.65237 16.9323 5.65237 17.5655 6.04289 17.956C6.43342 18.3465 7.06658 18.3465 7.45711 17.956L11.9987 13.4144L16.5408 17.9565C16.9313 18.347 17.5645 18.347 17.955 17.9565C18.3455 17.566 18.3455 16.9328 17.955 16.5423L13.4129 12.0002L17.955 7.45808C18.3455 7.06756 18.3455 6.43439 17.955 6.04387C17.5645 5.65335 16.9313 5.65335 16.5408 6.04387L11.9987 10.586L7.45711 6.04439C7.06658 5.65386 6.43342 5.65386 6.04289 6.04439C5.65237 6.43491 5.65237 7.06808 6.04289 7.4586L10.5845 12.0002L6.04289 16.5418Z"
              fill="" />
          </svg>
        </button>
        <div class="px-2 pr-14">
          <h4 class="mb-2 text-2xl font-semibold text-gray-800 dark:text-white/90">
            Create Complaint
          </h4>
          <p class="mb-6 text-sm text-gray-500 dark:text-gray-400 lg:mb-7">
            Create Complaint for Asset Management Multiprint
          </p>
        </div>
        <form class="flex flex-col">
          <div class="custom-scrollbar h-[458px] overflow-y-auto p-2">
            <div>

              <div class="grid grid-cols-1 gap-x-6 gap-y-5 lg:grid-cols-2">
                <div class="col-span-2 lg:col-span-1">
                  <label class="mb-1.5 block text-sm font-medium text-gray-700 dark:text-gray-400">
                    Code
                  </label>
                  <input type="text" value=""
                    class="dark:bg-dark-900 h-11 w-full rounded-lg border border-gray-300 bg-transparent bg-none px-4 py-2.5 text-sm text-gray-800 shadow-theme-xs placeholder:text-gray-400 focus:border-brand-300 focus:outline-hidden focus:ring-3 focus:ring-brand-500/10 dark:border-gray-700 dark:bg-gray-900 dark:text-white/90 dark:placeholder:text-white/30 dark:focus:border-brand-800" />
                </div>

                <div class="col-span-2 lg:col-span-1">
                  <label class="mb-1.5 block text-sm font-medium text-gray-700 dark:text-gray-400">
                    Customer
                  </label>
                  <input type="text" value=""
                    class="dark:bg-dark-900 h-11 w-full appearance-none rounded-lg border border-gray-300 bg-transparent bg-none px-4 py-2.5 text-sm text-gray-800 shadow-theme-xs placeholder:text-gray-400 focus:border-brand-300 focus:outline-hidden focus:ring-3 focus:ring-brand-500/10 dark:border-gray-700 dark:bg-gray-900 dark:text-white/90 dark:placeholder:text-white/30 dark:focus:border-brand-800" />
                </div>



                <div class="col-span-2">
                  <label class="mb-1.5 block text-sm font-medium text-gray-700 dark:text-gray-400">
                    Status
                  </label>
                  <select
                    class="dark:bg-dark-900 h-11 w-full appearance-none rounded-lg border border-gray-300 bg-transparent px-4 py-2.5 text-sm text-gray-800 shadow-theme-xs focus:border-brand-300 focus:outline-hidden focus:ring-3 focus:ring-brand-500/10 dark:border-gray-700 dark:bg-gray-900 dark:text-white/90 dark:focus:border-brand-800">
                    <option value="team-manager">Solved</option>
                    <option value="developer">In Progress</option>
                  </select>
                </div>
              </div>
            </div>
          </div>
          <div class="flex items-center gap-3 px-2 mt-6 lg:justify-end">
            <button @click="isProfileInfoModal = false" type="button"
              class="flex w-full justify-center rounded-lg border border-gray-300 bg-white px-4 py-2.5 text-sm font-medium text-gray-700 hover:bg-gray-50 dark:border-gray-700 dark:bg-gray-800 dark:text-gray-400 dark:hover:bg-white/[0.03] sm:w-auto">
              Close
            </button>
            <button @click="saveProfile" type="button"
              class="flex w-full justify-center rounded-lg bg-brand-500 px-4 py-2.5 text-sm font-medium text-white hover:bg-brand-600 sm:w-auto">
              Save Changes
            </button>
          </div>
        </form>
      </div>
    </template>
  </Modal>


  <div class="overflow-hidden rounded-xl border border-gray-200 bg-white dark:border-gray-800 dark:bg-white/[0.03]">
    <div class="max-w-full overflow-x-auto custom-scrollbar">
      <div>
      </div>
      <table class="min-w-full">
        <thead>
          <tr class="border-b border-gray-200 dark:border-gray-700">
            <th class="px-5 py-3 text-left w-3/11 sm:px-6">
              <p class="font-medium text-gray-500 text-theme-xs dark:text-gray-400">Code</p>
            </th>
            <th class="px-5 py-3 text-left w-2/11 sm:px-6">
              <p class="font-medium text-gray-500 text-theme-xs dark:text-gray-400">Customer</p>
            </th>
            <th class="px-5 py-3 text-left w-2/11 sm:px-6">
              <p class="font-medium text-gray-500 text-theme-xs dark:text-gray-400">Status</p>
            </th>
            <th class="px-5 py-3 text-left w-2/11 sm:px-6">
              <p class="font-medium text-gray-500 text-theme-xs dark:text-gray-400">Action</p>
            </th>
          </tr>
        </thead>
        <tbody class="divide-y divide-gray-200 dark:divide-gray-700">
          <tr v-for="(complaint, index) in complaints" :key="index"
            class="border-t border-gray-100 dark:border-gray-800">
            <td class="px-5 py-4 sm:px-6">
              <p class="text-gray-500 text-theme-sm dark:text-gray-400">{{ complaint.code }}</p>
            </td>
            <td class="px-5 py-4 sm:px-6">
              <p class="text-gray-500 text-theme-sm dark:text-gray-400">{{ complaint.customer }}</p>
            </td>
            <td class="px-5 py-4 sm:px-6">
              <p class="text-gray-500 text-theme-sm dark:text-gray-400">{{ complaint.status }}</p>
            </td>
            <td class="px-5 py-4 sm:px-6">
              <div class="flex items-center gap-2">
                <button class="edit-button">
                  <router-link to="/complaint/detail" class="block">
                    <span class="flex items-center gap-2">
                      <svg class="fill-current" width="18" height="18" viewBox="0 0 18 18" fill="none"
                        xmlns="http://www.w3.org/2000/svg">
                        <path fill-rule="evenodd" clip-rule="evenodd"
                          d="M15.0911 2.78206C14.2125 1.90338 12.7878 1.90338 11.9092 2.78206L4.57524 10.116C4.26682 10.4244 4.0547 10.8158 3.96468 11.2426L3.31231 14.3352C3.25997 14.5833 3.33653 14.841 3.51583 15.0203C3.69512 15.1996 3.95286 15.2761 4.20096 15.2238L7.29355 14.5714C7.72031 14.4814 8.11172 14.2693 8.42013 13.9609L15.7541 6.62695C16.6327 5.74827 16.6327 4.32365 15.7541 3.44497L15.0911 2.78206ZM12.9698 3.84272C13.2627 3.54982 13.7376 3.54982 14.0305 3.84272L14.6934 4.50563C14.9863 4.79852 14.9863 5.2734 14.6934 5.56629L14.044 6.21573L12.3204 4.49215L12.9698 3.84272ZM11.2597 5.55281L5.6359 11.1766C5.53309 11.2794 5.46238 11.4099 5.43238 11.5522L5.01758 13.5185L6.98394 13.1037C7.1262 13.0737 7.25666 13.003 7.35947 12.9002L12.9833 7.27639L11.2597 5.55281Z"
                          fill="black" />
                      </svg>
                      Detail
                    </span>
                  </router-link>
                </button>
                <button class="edit-button">
                  <router-link to="" class="block">
                    <span class="flex items-center gap-2">
                      Resolve
                    </span>
                  </router-link>
                </button>
              </div>
            </td>
          </tr>
        </tbody>
      </table>
    </div>
  </div>
</template>

<script setup>
import { ref } from 'vue';
import Modal from '../profile/Modal.vue'
import PlusIcon from '@/icons/PlusIcon.vue';
import FlagIcon from '@/icons/FlagIcon.vue';


// Modal Advanced Filter
const isAdvancedFilter = ref(false);
const AdvancedFilter = () => {
  console.log('Filter Opened')
  isAdvancedFilter.value = true
}


const complaints = ref([
  {
    code: 'CP/2025/06/0001',
    customer: 'Venus Dhamiko',
    status: 'Solved',
  },
  {
    code: 'CP/2025/06/0015',
    customer: 'Adhil Nov',
    status: 'In Progress',
  },


])
</script>

<style scoped>
/* Add any additional styles here if needed */
</style>
