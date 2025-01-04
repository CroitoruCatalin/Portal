<template>
  <div class="register">
    <h2>Register</h2>
    <form @submit.prevent="register">
      <input v-model="email" type="email" placeholder="Email" required />
      <input v-model="password" type="password" placeholder="Password" required />
      <input v-model="confirmPassword" type="password" placeholder="Confirm Password" required />
      <button type="submit">Register</button>
    </form>
    <p v-if="errorMessage" class="error">{{ errorMessage }}</p>
  </div>
</template>

<script lang="ts">
  import { defineComponent, ref } from 'vue';
  import api from '@/api';
  import { useRouter } from 'vue-router';

  export default defineComponent({
    name: 'RegisterPage',
    setup() {
      const router = useRouter();
      const email = ref('');
      const password = ref('');
      const confirmPassword = ref('');
      const errorMessage = ref('');

      const register = async () => {
        if (password.value !== confirmPassword.value) {
          errorMessage.value = 'Passwords do not match';
          return;
        }

        try {
          const response = await api.post('/user/register', {
            email: email.value,
            password: password.value,
            confirmPassword: confirmPassword.value,
          });
          console.log('Registration successful', response.data);
          router.push('/login');
        } catch (error: any) {
          errorMessage.value = error.response?.data?.error || 'An error occurred';
        }
      };

      return {
        email,
        password,
        confirmPassword,
        errorMessage,
        register,
      };
    },
  });
</script>

<style scoped>
  .error {
    color: red;
  }
</style>
