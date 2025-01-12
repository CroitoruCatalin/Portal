<template>
  <q-layout view="hHh Lpr lFf">
    <q-header elevated>
      <q-toolbar>
        <q-btn flat dense @click="navigateTo('home')" label="Home" />
        <q-btn flat dense @click="navigateTo('posts')" label="Posts" />
        <q-space />
        <q-btn flat dense color="negative" label="Logout" @click="logout" />
      </q-toolbar>
    </q-header>

    <q-page-container>
      <router-view />
    </q-page-container>
  </q-layout>
</template>

<script lang="ts">
  import { defineComponent } from 'vue';
  import { useRouter } from 'vue-router';
  import api from '@/api';

export default defineComponent({
  name: 'MainPage',
  setup() {
    const router = useRouter();

    const navigateTo = (page: string) => {
      router.push(`/${page}`);
    };

    const logout = async () => {
      try {
        const response = await api.post('/User/logout');
        console.log('Logout successful');
        router.push('/login');
      } catch (error) {
        console.error('Logout error:', error);
        if (error.response) {
          console.error('Response error:', error.response.data);
          alert('Logout failed: ' + error.response.data.error);
        } else {
          alert('An unexpected error occurred');
        }
      }
    };

    return {
      navigateTo,
      logout,
    };
  },
});
</script>
