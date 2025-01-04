<template>
  <div class="profile">
    <h2>Profile</h2>
    <p v-if="user">Welcome, {{ user.email }}!</p>
    <button @click="fetchProfile">Fetch Profile</button>
    <button @click="logout">Logout</button>
    <p v-if="errorMessage" class="error">{{ errorMessage }}</p>
  </div>
</template>

<script lang="ts">
  import { defineComponent, ref } from 'vue';
  import api from '@/api';
  import { useRouter } from 'vue-router';

interface User {
  email: string;
}

export default defineComponent({
  name: 'ProfilePage',
  setup() {
    const router = useRouter();
    const user = ref<User | null>(null);
    const errorMessage = ref('');

    const fetchProfile = async () => {
      try {
        const response = await api.get('/user/me');
        user.value = response.data;
      } catch (error: any) {
        errorMessage.value = error.response?.data?.error || 'An error occurred';
      }
    };

    const logout = async () => {
      try {
        await api.post('/User/logout');
        console.log('Logout successful');
        router.push('/login');
      } catch (error: any) {
        errorMessage.value = error.response?.data?.error || 'An error occurred';
      }
    }

    return {
      user,
      errorMessage,
      fetchProfile,
      logout,
    };
  },
});
</script>

<style scoped>
  .error {
    color: red;
  }
</style>
