<script>
import Sidebar from "@/components/Sidebar.vue";
export default {
  data() {
    return {
      sidebarActive: false,
    };
  },
  components: {
    Sidebar,
  },
  methods: {
    hideSidebar() {
      this.sidebarActive = false;
    },
    showSidebar() {
      this.sidebarActive = true;
    },
  },
};
</script>

<template>
  <Sidebar :active="sidebarActive" @close="hideSidebar()">
    <template v-slot:header>
      <router-link :to="{ name: 'Dashboard' }">
        <h1 class="text-black">JTS</h1>
      </router-link>
    </template>
    <template v-slot:content>
      <!--Dashboard-->
      <router-link class="sidebar-link" :to="{ name: 'Dashboard' }">
        <svg
          xmlns="http://www.w3.org/2000/svg"
          fill="none"
          viewBox="0 0 24 24"
          stroke-width="1.5"
          stroke="currentColor"
        >
          <path
            stroke-linecap="round"
            stroke-linejoin="round"
            d="M2.25 12l8.954-8.955c.44-.439 1.152-.439 1.591 0L21.75 12M4.5 9.75v10.125c0 .621.504 1.125 1.125 1.125H9.75v-4.875c0-.621.504-1.125 1.125-1.125h2.25c.621 0 1.125.504 1.125 1.125V21h4.125c.621 0 1.125-.504 1.125-1.125V9.75M8.25 21h8.25"
          />
        </svg>

        Dashboard
      </router-link>

      <!--Ticket-->
      <router-link class="sidebar-link" :to="{ name: 'Ticket' }">
        <svg
          xmlns="http://www.w3.org/2000/svg"
          fill="none"
          viewBox="0 0 24 24"
          stroke-width="1.5"
          stroke="currentColor"
        >
          <path
            stroke-linecap="round"
            stroke-linejoin="round"
            d="M16.5 6v.75m0 3v.75m0 3v.75m0 3V18m-9-5.25h5.25M7.5 15h3M3.375 5.25c-.621 0-1.125.504-1.125 1.125v3.026a2.999 2.999 0 010 5.198v3.026c0 .621.504 1.125 1.125 1.125h17.25c.621 0 1.125-.504 1.125-1.125v-3.026a2.999 2.999 0 010-5.198V6.375c0-.621-.504-1.125-1.125-1.125H3.375z"
          />
        </svg>

        Ticket
      </router-link>

      <a class="sidebar-link cursor-pointer">
        <svg
          xmlns="http://www.w3.org/2000/svg"
          fill="none"
          viewBox="0 0 24 24"
          stroke="currentColor"
        >
          <path
            stroke-linecap="round"
            stroke-linejoin="round"
            stroke-width="2"
            d="M17 16l4-4m0 0l-4-4m4 4H7m6 4v1a3 3 0 01-3 3H6a3 3 0 01-3-3V7a3 3 0 013-3h4a3 3 0 013 3v1"
          />
        </svg>
        Logout
      </a>
    </template>
  </Sidebar>

  <!-- Navbar -->
  <nav class="navbar shadow-xl h-16">
    <div class="container mx-auto flex p-3 items-center">
      <!-- Sidebar Toggle -->
      <button
        type="button"
        class="button button-transparent button-icon button-icon-md"
        @click="showSidebar()"
      >
        <svg
          xmlns="http://www.w3.org/2000/svg"
          viewBox="0 0 20 20"
          fill="currentColor"
        >
          <path
            fill-rule="evenodd"
            d="M3 5a1 1 0 011-1h12a1 1 0 110 2H4a1 1 0 01-1-1zM3 10a1 1 0 011-1h12a1 1 0 110 2H4a1 1 0 01-1-1zM3 15a1 1 0 011-1h12a1 1 0 110 2H4a1 1 0 01-1-1z"
            clip-rule="evenodd"
          />
        </svg>
      </button>

      <router-link :to="{ name: 'Dashboard' }" class="ml-3">
        <h3>JTS</h3>
      </router-link>
    </div>
  </nav>

  <router-view class="container mx-auto min-h-screen" />
</template>
