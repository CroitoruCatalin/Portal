<template>
  <q-page class="window-height window-width row justify-center items-center bg-grey-2">
    <div class="column q-pa-md" style="max-width: 400px;">

      <div class="row justify-center">
        <h5 class="text-h5 text-primary q-my-md">Portal Login</h5>
      </div>

      <q-card square bordered class="q-pa-lg shadow-2">
        <q-card-section>
          <q-form @submit.prevent="handleLogin" class="q-gutter-md">

            <q-input filled
                     v-model="username"
                     type="username"
                     label="Username"
                     required
                     :rules="[val => val && val.length >= 0 || 'Username is required']"
                     :lazy-rules
                     autofocus/>
                     
            <q-input filled
                     v-model="password"
                     type="password"
                     label="Password"
                     required
                     lazy-rules
                     :rules="[val => val && val.length > 0 || 'Password is required']" />

            <q-checkbox v-model="rememberMe" label="Remember Me" />
          </q-form>
        </q-card-section>

        <q-card-actions>
          <q-btn unelevated
                 color="primary"
                 size="lg"
                 class="full-width"
                 label="Login"
                 @click="handleLogin" />
        </q-card-actions>

        <q-card-section class="text-center q-pa-none">
          <p class="text-grey-6">Not registered? <q-btn flat label="Create an account" to="/register" /></p>
        </q-card-section>
      </q-card>
    </div>
  </q-page>
</template>

<script>
  import api from '@/api';
  export default {
    data() {
      return {
        username: '',
        password: '',
        rememberMe: false,
      };
    },
    methods: {
      async handleLogin() {
        try {
          const response = await api.post('/User/login', {
            username: this.username,
            password: this.password,
            rememberMe: this.rememberMe,
          });

          this.$router.push('/');
          console.log('Login successful!');
          console.log('Response:', response.data);
        } catch (error) {
          alert(error.message);
          console.error('Login error:', error);
        }
      },
    },
  };
</script>

<style>
  .q-btn.full-width {
    width: 100%;
  }
</style>
