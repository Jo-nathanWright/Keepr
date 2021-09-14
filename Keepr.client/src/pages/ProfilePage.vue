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
        <div class="d-flex mb-2">
          <h1>Vaults</h1>
          <h2 class="action ml-2 d-flex align-self-center" data-toggle="modal" data-target="#CreateVault" v-if="account.id === profile.id">
            ➕
          </h2>
        </div>
        <div class="card-columns">
          <VaultCard v-for="v in vaults" :key="v.id" :vault="v" />
        </div>
      </div>
    </div>
    <div class="row justify-content-center mt-5">
      <div class="col-11">
        <div class="d-flex mb-2">
          <h1>Keeps</h1>
          <h2 class="action ml-2 d-flex align-self-center" data-toggle="modal" data-target="#CreateKeep" v-if="account.id === profile.id">
            ➕
          </h2>
        </div>
        <div class="card-columns">
          <KeepCard v-for="k in keeps" :key="k.id" :keep="k" />
        </div>
      </div>
    </div>
  </div>

  <!-- Model For Vault Creation -->
  <div class="modal fade" id="CreateVault" tabindex="-1" aria-labelledby="CreateLabel" aria-hidden="true">
    <div class="modal-dialog modal-dialog-centered">
      <div class="modal-content border-info">
        <div class="modal-header">
          <h5 class="modal-title" id="CreateLabel">
            New Vault
          </h5>
          <button type="button" class="close" data-dismiss="modal" aria-label="Close">
            <span aria-hidden="true">&times;</span>
          </button>
        </div>
        <div class="modal-body">
          <form @submit.prevent="createVault()">
            <div class="form-group">
              <label for="vaultTitle">Title</label>
              <input type="text"
                     class="form-control"
                     id="vaultTitle"
                     v-model="state.newVault.name"
                     placeholder="Title..."
                     maxlength="28"
                     required
              >
            </div>
            <div class="form-group">
              <label for="vaultImg">Image URL</label>
              <textarea class="form-control"
                        id="vaultImg"
                        v-model="state.newVault.img"
                        placeholder="URL..."
                        required
              ></textarea>
            </div>
            <hr>
            <div class="d-flex justify-content-end">
              <button type="submit" data-toggle="modal" data-target="#CreateVault" class="btn btn-info mr-3">
                Create
              </button>
            </div>
          </form>
        </div>
      </div>
    </div>
  </div>

  <!-- Model For Keep Creation -->
  <div class="modal fade" id="CreateKeep" tabindex="-1" aria-labelledby="CreateLabel" aria-hidden="true">
    <div class="modal-dialog modal-dialog-centered">
      <div class="modal-content border-info">
        <div class="modal-header">
          <h5 class="modal-title" id="CreateLabel">
            New Keep
          </h5>
          <button type="button" class="close" data-dismiss="modal" aria-label="Close">
            <span aria-hidden="true">&times;</span>
          </button>
        </div>
        <div class="modal-body">
          <form @submit.prevent="createKeep()">
            <div class="form-group">
              <label for="keepTitle">Title</label>
              <input type="text"
                     class="form-control"
                     id="keepTitle"
                     v-model="state.newKeep.name"
                     placeholder="Title..."
                     maxlength="28"
                     required
              >
            </div>
            <div class="form-group">
              <label for="keepImg">Image URL</label>
              <textarea class="form-control"
                        id="keepImg"
                        v-model="state.newKeep.img"
                        placeholder="URL..."
                        required
              ></textarea>
            </div>
            <div class="form-group">
              <label for="keepDescription">Description</label>
              <textarea class="form-control"
                        id="keepDescription"
                        v-model="state.newKeep.description"
                        placeholder="Keep Description..."
                        rows="4"
                        maxlength="1000"
                        required
              ></textarea>
            </div>
            <hr>
            <div class="d-flex justify-content-end">
              <button type="submit" data-toggle="modal" data-target="#CreateKeep" class="btn btn-info mr-3">
                Create
              </button>
            </div>
          </form>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { computed, onMounted, reactive } from 'vue'
import { AppState } from '../AppState'
import { profileService } from '../services/ProfileService'
import Pop from '../utils/Notifier'
import { useRoute } from 'vue-router'
import { keepsService } from '../services/KeepsService'
export default {
  name: 'Profile',
  setup() {
    const route = useRoute()
    const state = reactive({
      newKeep: {},
      newVault: {}
    })
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
      state,
      profile: computed(() => AppState.profile),
      vaults: computed(() => AppState.profileVaults),
      keeps: computed(() => AppState.profileKeeps),
      account: computed(() => AppState.account),
      async createKeep() {
        try {
          await keepsService.Create(state.newKeep)
          await profileService.getKeepsByProfile(route.params.id)
        } catch (error) {
          Pop.toast(error, 'error')
        }
      }
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
.action{
  cursor: pointer;
}
</style>
