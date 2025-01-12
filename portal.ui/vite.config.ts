import { defineConfig } from 'vite';
import vue from '@vitejs/plugin-vue';
import path from 'path';

// https://vitejs.dev/config/
export default defineConfig({
  plugins: [vue()],
  resolve: {
    alias: {
      '@': path.resolve(__dirname, 'src'),
      '@styles': path.resolve(__dirname, 'src/styles'),
      '@quasar': path.resolve(__dirname, 'node_modules/quasar'),
    },
  },
  css: {
    preprocessorOptions: {
      scss: {
        additionalData: `@use 'quasar/src/css/variables.sass' as *;`,
      },
    },
  },
  server: {
    port: 33052, // Your custom port, if needed
  },
});
