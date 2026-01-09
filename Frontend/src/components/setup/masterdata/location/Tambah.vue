<template>
    <div class="col-span-2 ">

        <div class="flex mb-4 gap-6">
            <div class="w-1/2">
                <label class="mb-1.5 block text-sm font-medium text-gray-700 dark:text-gray-400">
                    ID
                </label>

                <div class="w-full">
                    <input type="text" value=""
                        class="dark:bg-dark-900 h-11 w-full appearance-none rounded-lg border border-gray-300 bg-transparent bg-none px-4 py-2.5 text-sm text-gray-800 shadow-theme-xs placeholder:text-gray-400 focus:border-brand-300 focus:outline-hidden focus:ring-3 focus:ring-brand-500/10 dark:border-gray-700 dark:bg-gray-900 dark:text-white/90 dark:placeholder:text-white/30 dark:focus:border-brand-800" />
                </div>
            </div>
            <div class="w-1/2">
                <label class="mb-1.5 block text-sm font-medium text-gray-700 dark:text-gray-400">
                    Tipe
                </label>

                <div class="w-full">
                    <select
                        class="dark:bg-dark-900 h-11 w-full appearance-none rounded-lg border border-gray-300 bg-transparent px-4 py-2.5 text-sm text-gray-800 shadow-theme-xs focus:border-brand-300 focus:outline-hidden focus:ring-3 focus:ring-brand-500/10 dark:border-gray-700 dark:bg-gray-900 dark:text-white/90 dark:focus:border-brand-800">
                        <option value="">Warehouse</option>
                        <option value="">Customer</option>
                        <option value="">Workshop</option>
                    </select>
                </div>
            </div>

        </div>
        <div class="flex mb-4 gap-6">
            <div class="w-1/2">
                <label class="mb-1.5 block text-sm font-medium text-gray-700 dark:text-gray-400">
                    Printer
                </label>

                <div class="w-full">
                    <select
                        class="dark:bg-dark-900 h-11 w-full appearance-none rounded-lg border border-gray-300 bg-transparent px-4 py-2.5 text-sm text-gray-800 shadow-theme-xs focus:border-brand-300 focus:outline-hidden focus:ring-3 focus:ring-brand-500/10 dark:border-gray-700 dark:bg-gray-900 dark:text-white/90 dark:focus:border-brand-800">
                        <option value="">Beli Putus</option>
                        <option value="">KSO</option>

                    </select>
                </div>
            </div>
            <div class="w-1/2">
                <label class="mb-1.5 block text-sm font-medium text-gray-700 dark:text-gray-400">
                    Toner
                </label>

                <div class="w-full">
                    <select
                        class="dark:bg-dark-900 h-11 w-full appearance-none rounded-lg border border-gray-300 bg-transparent px-4 py-2.5 text-sm text-gray-800 shadow-theme-xs focus:border-brand-300 focus:outline-hidden focus:ring-3 focus:ring-brand-500/10 dark:border-gray-700 dark:bg-gray-900 dark:text-white/90 dark:focus:border-brand-800">
                        <option value="">Beli Putus</option>
                        <option value="">KSO</option>

                    </select>
                </div>
            </div>

        </div>

        <div class="flex items-center gap-3 px-2 mt-6 lg:justify-start w-full">
            <button @click="goBack" type="button"
                class="flex w-full justify-center rounded-lg border border-gray-300 bg-white px-4 py-2.5 text-sm font-medium text-gray-700 hover:bg-gray-50 dark:border-gray-700 dark:bg-gray-800 dark:text-gray-400 dark:hover:bg-white/[0.03] sm:w-auto">
                Batal
            </button>
            <button @click="saveProfile" type="button"
                class="flex w-full justify-center rounded-lg bg-brand-500 px-4 py-2.5 text-sm font-medium text-white hover:bg-brand-600 sm:w-auto">
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
