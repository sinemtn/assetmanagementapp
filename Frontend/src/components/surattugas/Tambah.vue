<template>
  <div class="col-span-2 ">

    <div class="flex mb-4 gap-6">

      <div class="w-1/2">
      <label class="mb-1.5 block text-sm font-medium text-gray-700 dark:text-gray-400">
        Customer
    </label>

    <div class="relative w-full mb-4">
    <!-- Input field -->
    <input
        v-model="caricustomer"
        type="text"
        placeholder="Cari Customer"
        class="h-11 w-full rounded-lg border border-gray-300 bg-transparent px-4 pr-10 py-2.5 text-sm text-gray-800 shadow-theme-xs placeholder:text-gray-400 focus:border-brand-300 focus:outline-hidden focus:ring-3 focus:ring-brand-500/10 dark:border-gray-700 dark:bg-gray-900 dark:text-white/90 dark:placeholder:text-white/30"
        @keydown.enter="searchCustomer"
    />

    <!-- Search button -->
    <button
        type="button"
        @click="toggleSearchCustomer"
        class="absolute right-3 top-1/2 -translate-y-1/2 text-gray-500 hover:text-brand-600"
    >
        <svg
        xmlns="http://www.w3.org/2000/svg"
        class="h-5 w-5"
        fill="none"
        viewBox="0 0 24 24"
        stroke="currentColor"
        >
        <path
            stroke-linecap="round"
            stroke-linejoin="round"
            stroke-width="2"
            d="M21 21l-4.35-4.35M11 18a7 7 0 100-14 7 7 0 000 14z"
        />
        </svg>
    </button>

    <div
        v-if="showCariCustomer"
        class="absolute left-0 z-50 mt-1 w-full max-h-48 overflow-auto rounded-md border border-gray-200 bg-white shadow-md dark:border-gray-700 dark:bg-gray-800"
    >
        <ul>
        <li
            v-for="(item, index) in filteredCariCustomers"
            :key="index"
            @click="selectCustomer(item)"
            class="cursor-pointer px-3 py-2 text-sm text-gray-800 hover:bg-gray-100 dark:text-white dark:hover:bg-gray-700"
        >
            {{ item }}
        </li>

        <li
            v-if="filteredCariCustomers.length === 0"
            class="px-3 py-2 text-sm text-gray-500 dark:text-gray-400"
        >
            Customer Tidak Ditemukan
        </li>
        </ul>
    </div>
    </div>
      </div>
      <!-- NO. MP -->

      <!-- Printer -->
      <div class="w-1/2">
      <label class="mb-1.5 block text-sm font-medium text-gray-700 dark:text-gray-400">
          No. Mp
      </label>

      <div class="w-full" >
       <input
                type="text"
                value="No MP Customer1"
                class="dark:bg-dark-900 h-11 w-full appearance-none rounded-lg border border-gray-300 bg-transparent bg-none px-4 py-2.5 text-sm text-gray-800 shadow-theme-xs placeholder:text-gray-400 focus:border-brand-300 focus:outline-hidden focus:ring-3 focus:ring-brand-500/10 dark:border-gray-700 dark:bg-gray-900 dark:text-white/90 dark:placeholder:text-white/30 dark:focus:border-brand-800"
                disabled
                />
      </div>
      </div>
      <!-- End of Printer -->
    </div>

    <div class="flex mb-4 gap-6">
    <!-- Serial No -->
      <div class="w-1/2">
      <label class="mb-1.5 block text-sm font-medium text-gray-700 dark:text-gray-400">
          Tipe
      </label>
        <div class="w-full" >
          
            <select
                class="dark:bg-dark-900 h-11 w-full appearance-none rounded-lg border border-gray-300 bg-transparent px-4 py-2.5 text-sm text-gray-800 shadow-theme-xs focus:border-brand-300 focus:outline-hidden focus:ring-3 focus:ring-brand-500/10 dark:border-gray-700 dark:bg-gray-900 dark:text-white/90 dark:focus:border-brand-800"
                >
                <option value=""> Maintenance </option>
                <option value=""> Service </option>
            </select>
        </div>
      </div>
      <!-- Serial No -->

     <!-- NO. MP -->
      <div class="w-1/2">
      <label class="mb-1.5 block text-sm font-medium text-gray-700 dark:text-gray-400">
        PIC
    </label>
    <div class="relative w-full mb-4">
    <!-- Input field -->
    <input
        v-model="tipeprinter"
        type="text"
        placeholder="Cari PIC"
        class="h-11 w-full rounded-lg border border-gray-300 bg-transparent px-4 pr-10 py-2.5 text-sm text-gray-800 shadow-theme-xs placeholder:text-gray-400 focus:border-brand-300 focus:outline-hidden focus:ring-3 focus:ring-brand-500/10 dark:border-gray-700 dark:bg-gray-900 dark:text-white/90 dark:placeholder:text-white/30"
        @keydown.enter="searchTipePrinter"
    />

    <!-- Search button -->
    <button
        type="button"
        @click="toggleSearchTipePrinter"
        class="absolute right-3 top-1/2 -translate-y-1/2 text-gray-500 hover:text-brand-600"
    >
        <svg
        xmlns="http://www.w3.org/2000/svg"
        class="h-5 w-5"
        fill="none"
        viewBox="0 0 24 24"
        stroke="currentColor"
        >
        <path
            stroke-linecap="round"
            stroke-linejoin="round"
            stroke-width="2"
            d="M21 21l-4.35-4.35M11 18a7 7 0 100-14 7 7 0 000 14z"
        />
        </svg>
    </button>

    <div
        v-if="showTipePrinter"
        class="absolute left-0 z-50 mt-1 w-full max-h-48 overflow-auto rounded-md border border-gray-200 bg-white shadow-md dark:border-gray-700 dark:bg-gray-800"
    >
        <ul>
        <li
            v-for="(item, index) in filteredTipePrinters"
            :key="index"
            @click="selectTipePrinter(item)"
            class="cursor-pointer px-3 py-2 text-sm text-gray-800 hover:bg-gray-100 dark:text-white dark:hover:bg-gray-700"
        >
            {{ item }}
        </li>

        <li
            v-if="filteredTipePrinters.length === 0"
            class="px-3 py-2 text-sm text-gray-500 dark:text-gray-400"
        >
            Tipe Printer Tidak Ditemukan
        </li>
        </ul>
    </div>
    </div>
      </div>
      <!-- NO. MP -->

    </div>


    <!-- Active -->
    <label class="mb-1.5 block text-sm font-medium text-gray-700 dark:text-gray-400">
        Status
    </label>
    <div class="w-full mb-4" >
    <select
        class="dark:bg-dark-900 h-11 w-full appearance-none rounded-lg border border-gray-300 bg-transparent px-4 py-2.5 text-sm text-gray-800 shadow-theme-xs focus:border-brand-300 focus:outline-hidden focus:ring-3 focus:ring-brand-500/10 dark:border-gray-700 dark:bg-gray-900 dark:text-white/90 dark:focus:border-brand-800"
        >
        <option value=""> True </option>
        <option value=""> False </option>
    </select>
    </div>
    <!-- End of Active -->

 
    <div class="flex items-center gap-3 px-2 mt-6 lg:justify-start w-full">
        <button
        @click="goBack"
        type="button"
        class="flex w-full justify-center rounded-lg border border-gray-300 bg-white px-4 py-2.5 text-sm font-medium text-gray-700 hover:bg-gray-50 dark:border-gray-700 dark:bg-gray-800 dark:text-gray-400 dark:hover:bg-white/[0.03] sm:w-auto"
        >
        Batal
        </button>
        <button
        @click="saveProfile"
        type="button"
        class="flex w-full justify-center rounded-lg bg-brand-500 px-4 py-2.5 text-sm font-medium text-white hover:bg-brand-600 sm:w-auto"
        >
        Simpan
        </button>
    </div>
  </div>
</template>

<script setup>
import { ref } from 'vue'
import { useRouter } from 'vue-router'

// Go Back
const router = useRouter()
const goBack = () => {
    router.back()
}

// Cari Customer
const caricustomer = ref('')
const showCariCustomer = ref(false)
const filteredCariCustomers = ref([])

const caricustomers = ref([
  'Tah Sung Hung',
  'Venus Dhammiko',
  'Ardhito Pramono Anung',
  
])

const searchCustomer = () => {
  filteredCariCustomers.value = caricustomers.value.filter(c =>
    c.toLowerCase().includes(caricustomer.value.toLowerCase())
  )
  showCariCustomer.value = true
}


const toggleSearchCustomer = () => {
  showCariCustomer.value = !showCariCustomer.value
  if (showCariCustomer.value) searchCustomer()
}

const selectCustomer = (name) => {
  caricustomer.value = name
  showCariCustomer.value = false
}



const tipeprinter = ref('')
const showTipePrinter = ref(false)
const filteredTipePrinters = ref([])

const tipeprinters = ref([
  'HP LJ PRO200 M201',
  'HP LaserJet P3015',
  'HP korsta X956',
])

const searchTipePrinter = () => {
  filteredTipePrinters.value = tipeprinters.value.filter(c =>
    c.toLowerCase().includes(tipeprinter.value.toLowerCase())
  )
  showTipePrinter.value = true
}

const toggleSearchTipePrinter = () => {
  showTipePrinter.value = !showTipePrinter.value
  if (showTipePrinter.value) searchTipePrinter()
}

const selectTipePrinter = (name) => {
  tipeprinter.value = name
  showTipePrinter.value = false
}




const PIC = ref('')
const showPICList = ref(false)
const filteredPICs = ref([])

const PICs = ref([
  'Mortekiano Nigel',
  'Adhil Jaidi',
  'Venus Saturnus',
  'Sandra Dewi',
])

const searchPIC = () => {
  filteredPICs.value = PICs.value.filter(c =>
    c.toLowerCase().includes(PIC.value.toLowerCase())
  )
  showPICList.value = true
}

const toggleSearchPIC = () => {
  showPICList.value = !showPICList.value
  if (showPICList.value) searchPIC()
}

const selectPIC = (name) => {
  PIC.value = name
  showPICList.value = false
}

const printerst = ref('')
const showprinterstList = ref(false)
const filteredprintersts = ref([])

const printersts = ref([
  'Mortekiano Nigel',
  'Adhil Jaidi',
  'Venus Saturnus',
  'Sandra Dewi',
])

const searchprinterstd = () => {
  filteredprinterstds.value = printerstds.value.filter(c =>
    c.toLowerCase().includes(printerstd.value.toLowerCase())
  )
  showprinterstdList.value = true
}

const toggleSearchprinterstd = () => {
  showprinterstdList.value = !showprinterstdList.value
  if (showprinterstdList.value) searchprinterstd()
}

const selectprinterstd = (name) => {
  printerstd.value = name
  showprinterstdList.value = false
}


const openSection = ref(null)

const toggleSection = (section) => {
  openSection.value = openSection.value === section ? null : section
}

const printerstds = ref([
    {
        tipe: 'HP LJ PRO200 M201',
        id: 'M401-001d',
        serialno: 'SN00012334343',
    },
    {
        tipe: 'ASUS LG PRO190 M908',
        id: 'M402-001d',
        serialno: 'SN00012334365',
    },
])
</script>
