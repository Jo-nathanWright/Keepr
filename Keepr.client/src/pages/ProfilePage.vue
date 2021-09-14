<template>
  <div class="container-fluid">
    <div class="row justify-content-center mt-5">
      <div class="col-11 d-flex flex-md-row flex-column">
        <img class="rounded" :src="profile.picture" :alt="profile.name + '\'s Image'">
        <div class="ml-md-5">
          <h3>
            {{ profile.name }}
          </h3>
          <h4>Vaults: {{ vaults.length }}</h4>
          <h4>Keeps: {{ keeps.length }} </h4>
        </div>
      </div>
    </div>
    <div class="row justify-content-center mt-5">
      <div class="col-11">
        <h1>Keeps</h1>
        <div class="card-columns">
          <!-- Bootstrap Cards show how to use this -->
          <Card v-for="k in keeps" :key="k.id" :keep="k" />
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { computed, onMounted } from 'vue'
import { AppState } from '../AppState'
import { profileService } from '../services/ProfileService'
import Pop from '../utils/Notifier'
import { useRoute } from 'vue-router'
export default {
  name: 'Profile',
  setup() {
    const route = useRoute()
    onMounted(async() => {
      try {
        await profileService.getProfile(route.params.id)
        await profileService.getVaultsByProfile(route.params.id)
        await profileService.getKeepsByProfile(route.params.id)
      } catch (error) {
        Pop.toast(error, 'error')
      }
    })
    return {
      profile: computed(() => AppState.profile),
      vaults: computed(() => AppState.profileVaults),
      keeps: computed(() => AppState.profileKeeps)
    }
  }
}
</script>

<style scoped>
img {
  max-width: 125px;
}
@media only screen and (min-width: 720px){
  .card-columns{
    column-count: 5;
  }
}
</style>
