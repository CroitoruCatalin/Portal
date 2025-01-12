import { createRouter, createWebHistory} from 'vue-router';
import RegisterPage from '@/components/RegisterPage.vue';
import LoginPage from '@/components/LoginPage.vue';
import MainPage from '@/components/MainPage.vue';
import HomePage from '@/components/HomePage.vue';
import PostsPage from '@/components/PostsPage.vue';

import api from '@/api';

const routes = [
  { path: '/login', component: LoginPage },
  { path: '/register', component: RegisterPage },
  {
    path: '/',
    component: MainPage,
    children: [
      { path: '', component: HomePage },
      { path: 'home', component: HomePage },
      { path: 'posts', component: PostsPage },
    ],
  },
  { path: '/:catchAll(.*)', redirect: '/login' },
];

const router = createRouter({
  history: createWebHistory(),
  routes,
});

router.beforeEach(async (to, from, next) => {
  const protectedRoutes = ['/', '/home', '/posts'];
  const publicRoutes = ['/login', '/register'];

  if (publicRoutes.includes(to.path)) {
    return next();
  }

  if (protectedRoutes.some(route => to.path.startsWith(route))) {
    try {
      await api.get('/User/me');
      next();
    } catch (error) {
      console.error('Authentication error:', error);
      next('/login');
    }
  } else {
    next();
  }
});



export default router;
