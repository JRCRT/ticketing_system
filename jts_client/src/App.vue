<script>
import Sidebar from "@/components/Sidebar.vue";
import { ref, computed } from "vue";
import { useRouter } from "vue-router";
import { useStore } from "vuex";
import { TICKET_STATUS, ROLE } from "@/util/constant";
import Loader from "@/components/Loader.vue";
import Alert from "@/components/Alert.vue";
export default {
  components: {
    Sidebar,
    Alert,
    Loader,
  },

  setup() {
    const router = useRouter();
    const store = useStore();
    const sidebarActive = ref(false);
    const VITE_APP_TITLE = ref(import.meta.env.VITE_APP_TITLE);
    store.commit(
      "app/SET_CURRENT_USER",
      JSON.parse(localStorage.getItem("user"))
    );

    const hideSidebar = () => {
      sidebarActive.value = false;
    };

    const showSidebar = () => {
      sidebarActive.value = true;
    };

    const removeAlert = (index) => {
      store.commit("app/REMOVE_ALERT", index);
    };

    const currentUser = computed(() => store.state.app.currentUser);
    const hideNavbar = computed(() => store.state.app.hideNavbar);
    const alerts = computed(() => store.state.app.alerts);
    const isLoading = computed(() => store.state.app.isLoading);
    const logout = () => {
      store.dispatch("auth/logout");
      router.replace({ name: "Login" });
    };

    return {
      sidebarActive,
      VITE_APP_TITLE,
      store,
      hideSidebar,
      showSidebar,
      removeAlert,
      logout,
      hideNavbar,
      alerts,
      isLoading,
      TICKET_STATUS,
      currentUser,
      ROLE,
    };
  },
};
</script>

<template>
  <Loader v-if="isLoading" />
  <Sidebar :active="sidebarActive" @close="hideSidebar">
    <template v-slot:header>
      <router-link :to="{ name: 'Dashboard' }">
        <h1 class="text-black">{{ VITE_APP_TITLE }}</h1>
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

      <router-link
        v-if="currentUser?.role.name === ROLE.ADMIN"
        class="sidebar-link"
        :to="{ name: 'Ticket', params: { status: TICKET_STATUS.PENDING } }"
      >
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
        All Tickets
      </router-link>

      <router-link
        v-if="currentUser?.role.name !== ROLE.APPROVER"
        class="sidebar-link"
        :to="{ name: 'MyTicket', params: { status: TICKET_STATUS.PENDING } }"
      >
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
        My Ticket
      </router-link>

      <router-link
        v-if="currentUser?.role.name !== ROLE.USER"
        class="sidebar-link"
        :to="{
          name: 'TicketForApproval',
          params: { status: TICKET_STATUS.PENDING },
        }"
      >
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
        Ticket For Approval
      </router-link>

      <!--Role Manager-->
      <!--  <router-link class="sidebar-link" :to="{ name: 'RoleManager' }">
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
            d="M15 19.128a9.38 9.38 0 002.625.372 9.337 9.337 0 004.121-.952 4.125 4.125 0 00-7.533-2.493M15 19.128v-.003c0-1.113-.285-2.16-.786-3.07M15 19.128v.106A12.318 12.318 0 018.624 21c-2.331 0-4.512-.645-6.374-1.766l-.001-.109a6.375 6.375 0 0111.964-3.07M12 6.375a3.375 3.375 0 11-6.75 0 3.375 3.375 0 016.75 0zm8.25 2.25a2.625 2.625 0 11-5.25 0 2.625 2.625 0 015.25 0z"
          />
        </svg>

        Role Manager
      </router-link> -->

      <!--User-->
      <router-link
        v-if="currentUser?.role.name === ROLE.ADMIN"
        class="sidebar-link"
        :to="{ name: 'User' }"
      >
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
            d="M15 19.128a9.38 9.38 0 002.625.372 9.337 9.337 0 004.121-.952 4.125 4.125 0 00-7.533-2.493M15 19.128v-.003c0-1.113-.285-2.16-.786-3.07M15 19.128v.106A12.318 12.318 0 018.624 21c-2.331 0-4.512-.645-6.374-1.766l-.001-.109a6.375 6.375 0 0111.964-3.07M12 6.375a3.375 3.375 0 11-6.75 0 3.375 3.375 0 016.75 0zm8.25 2.25a2.625 2.625 0 11-5.25 0 2.625 2.625 0 015.25 0z"
          />
        </svg>

        Users
      </router-link>

      <a class="sidebar-link cursor-pointer" @click="logout">
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
  <nav v-if="!hideNavbar" class="navbar shadow-xl h-16">
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
          fill="black"
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

      <div class="ml-auto my-auto flex items-center">
        <div class="mr-5 font-medium text-sm">{{ currentUser?.username }}</div>
        <button
          type="button"
          class="button button-transparent button-icon button-icon-sm"
        >
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
              d="M14.857 17.082a23.848 23.848 0 005.454-1.31A8.967 8.967 0 0118 9.75v-.7V9A6 6 0 006 9v.75a8.967 8.967 0 01-2.312 6.022c1.733.64 3.56 1.085 5.455 1.31m5.714 0a24.255 24.255 0 01-5.714 0m5.714 0a3 3 0 11-5.714 0"
            />
          </svg>
        </button>
      </div>
    </div>
  </nav>

  <div class="alerts">
    <transition-group name="fade">
      <div v-for="(alert, index) in alerts" :key="alert">
        <Alert :alert="alert" @remove="removeAlert(index)" />
      </div>
    </transition-group>
  </div>
  <router-view class="container mx-auto h-full px-4 mt-6" />
</template>
