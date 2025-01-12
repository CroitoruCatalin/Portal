<template>
  <q-page class="window-height window-width row justify-center items-center bg-grey-2">
    <div class="column q-pa-md" style="max-width: 400px;">

      <div class="row justify-center">
        <h5 class="text-h5 text-primary q-my-md">Create an Account</h5>
      </div>

      <q-card square bordered class="q-pa-lg shadow-2">
        <q-card-section>
          <q-form @submit.prevent="register" class="q-gutter-md">

            <q-input filled
                     v-model="username"
                     type="text"
                     label="Username"
                     required
                     :rules="[val => val && val.length >= 3 || 'Username must be at least 3 characters']"
                     autofocus />

            <q-input filled
                     v-model="email"
                     type="email"
                     label="Email"
                     required
                     :rules="[val => val && /.+@.+\..+/.test(val) || 'Invalid email']" />

            <q-input filled
                     v-model="password"
                     type="password"
                     label="Password"
                     required
                     :rules="[val => val && val.length >= 6 || 'Password must be at least 6 characters']" />

            <q-input filled
                     v-model="confirmPassword"
                     type="password"
                     label="Confirm Password"
                     required
                     :rules="[val => val === password || 'Passwords must match']" />

            <p v-if="errorMessage" class="text-negative text-body2">{{ errorMessage }}</p>
          </q-form>
        </q-card-section>

        <q-card-actions>
          <q-btn unelevated
                 color="primary"
                 size="lg"
                 class="full-width"
                 label="Register"
                 @click="register" />
        </q-card-actions>

        <q-card-section class="text-center q-pa-none">
          <p class="text-grey-6">
            Already have an account?
            <q-btn flat label="Login" to="/login" />
          </p>
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
        email: '',
        password: '',
        confirmPassword: '',
        errorMessage: '',
      };
    },
    methods: {
      async register() {
        if (this.password !== this.confirmPassword) {
          this.errorMessage = 'Passwords do not match';
          return;
        }

        try {
          const response = await api.post('/user/register', {
            username: this.username,
            email: this.email,
            password: this.password,
            confirmPassword: this.confirmPassword,
          });

          this.$router.push('/login');
          console.log('Registration successful!', response.data);
        } catch (error) {
          this.errorMessage =
            error.response?.data?.error || 'An error occurred during registration';
          console.error('Registration error:', error);
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
