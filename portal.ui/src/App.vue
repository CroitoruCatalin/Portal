<template>
  <q-layout>
    <q-page-container>
      <router-view></router-view>
    </q-page-container>
  </q-layout>

</template>

<script>
  import { ref } from 'vue';
  import HelloWorld from './components/HelloWorld.vue';
  import { Quasar } from 'quasar';

export default {
  name: 'LayoutDefault',

  components: {
    HelloWorld
  },

  setup () {
    return {
      leftDrawerOpen: ref(false)
    }
  }
}
</script>
