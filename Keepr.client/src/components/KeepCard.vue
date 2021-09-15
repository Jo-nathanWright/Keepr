<template>
  <div class="card" data-toggle="modal" :data-target="'#m' + keep.id" @click="getKeepId(keep.id), updateViews(keep.id)">
    <img class="card-img-top" :src="keep.img" alt="Card image cap">
    <div class="card-img-overlay text-light d-flex align-items-end justify-content-between">
      <h5 class="card-title">
        {{ keep.name }}
      </h5>
      <img class="image rounded-circle" :src="keep.creator.picture" :alt="keep.name">
    </div>
  </div>

  <div class="modal fade bd-example-modal-lg"
       :id="'m' + keep.id"
       tabindex="-1"
       role="dialog"
       aria-labelledby="myLargeModalLabel"
       aria-hidden="true"
  >
    <div class="modal-dialog modal-lg modal-dialog-centered">
      <div class="modal-content">
        <div class="row justify-content-around">
          <div class="col-6 my-2 d-flex align-items-center">
            <img class="card-img-top keepImage rounded" :src="keep.img" alt="Card image cap">
          </div>
          <div class="col-5 d-flex flex-column justify-content-between my-3">
            <div class=" border-bottom border-dark mb-2">
              <div class="text-center mt-4">
                {{ keep.views }} Views
                {{ keep.keeps }} Keeps
              </div>
              <div class="text-center mt-4">
                <h3>{{ keep.name }}</h3>
              </div>
              <div class="my-4">
                <p>
                  {{ keep.description }}
                </p>
              </div>
            </div>
            <div>
              <div class="d-flex justify-content-between" v-if="account.id != null">
                <div class="btn-group dropup" @click="getVaults(account.id)">
                  <button type="button" class="btn btn-outline-success dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                    Add to Vault
                  </button>
                  <div class="dropdown-menu">
                    <DropDown v-for="v in vaults" :key="v.id" :vault="v" />
                  </div>
                </div>
                <h5 class="d-flex align-self-center action" v-if="account.id === keep.creatorId" @click="destroy(keep.id, account.id)" data-toggle="modal" :data-target="'#m' + keep.id">
                  🗑
                </h5>
                <router-link :to="{ name: 'Profile', params: {id: keep.creatorId} }">
                  <img class="rounded-circle image mr-2" :src="keep.creator.picture" alt="keep.creator.name" data-dismiss="modal">
                </router-link>
              </div>
              <div class="d-flex justify-content-end" v-else>
                <router-link :to="{ name: 'Profile', params: {id: keep.creatorId} }">
                  <img class="rounded-circle image mr-2" :src="keep.creator.picture" alt="keep.creator.name" data-dismiss="modal">
                </router-link>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { computed, reactive } from '@vue/runtime-core'
import { AppState } from '../AppState'
import { keepsService } from '../services/KeepsService'
import { profileService } from '../services/ProfileService'
import Pop from '../utils/Notifier'
export default {
  props: {
    keep: {
      type: Object,
      required: true
    }
  },
  setup() {
    const state = reactive({
      editedKeep: {}
    })
    return {
      state,
      account: computed(() => AppState.account),
      vaults: computed(() => AppState.profileVaults),
      vault: computed(() => AppState.activeVault),
      async destroy(keepId, userId) {
        try {
          if (await Pop.confirm()) {
            await keepsService.Delete(keepId)
            await keepsService.getAll()
            await profileService.getKeepsByProfile(userId)
            Pop.toast('That keep has been Deleted')
          }
        } catch (error) {
          Pop.toast(error, 'error')
        }
      },
      async getVaults(userId) {
        try {
          await profileService.getVaultsByProfile(userId)
        } catch (error) {
          Pop.toast(error, 'error')
        }
      },
      async getKeepId(keepId) {
        try {
          await keepsService.getById(keepId)
        } catch (error) {
          Pop.toast(error)
        }
      },
      async updateViews(keepId) {
        try {
          await keepsService.getById(keepId)
          const fullKeep = AppState.activeKeep
          state.editedKeep.views = fullKeep.views + 1
          await keepsService.editViewsorKeeps(fullKeep.id, state.editedKeep)
        } catch (error) {
          Pop.toast('Error', error)
        }
      }
    }
  }
}
</script>

<style scoped lang="scss">
.home{
  text-align: center;
  user-select: none;
  > img{
    height: 200px;
    width: 200px;
  }
}
.image{
  max-height: 50px;
}
.keepImage{
  max-height: 450px;
  min-height: 225px;
  object-fit: cover;
}
.action{
  cursor: pointer;
}
</style>
