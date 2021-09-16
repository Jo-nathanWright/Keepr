<template>
  <div class="card zinx" data-toggle="modal" :data-target="'#m' + keep.id" @click="getKeepId(keep.id)">
    <img class="card-img-top" :src="keep.img" alt="Card image cap">

    <div class="card-img-overlay text-light d-flex align-items-end justify-content-between">
      <h5 class="card-title">
        {{ keep.name }}
      </h5>
      <img class="image rounded-circle" :src="keep.creator.picture" :alt="keep.creator.name">
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
        <div class="row justify-md-content-around justify-content-center">
          <div class="col-md-6 col-11 my-2 d-flex align-items-center">
            <img class="card-img-top keepImage rounded" :src="keep.img" alt="Card image cap">
          </div>
          <div class="col-md-5 col-11 d-flex flex-column justify-content-between my-3">
            <div v-if="canDelete === true && vault.creatorId === account.id" class="d-flex justify-content-end">
              <h5 class="action" @click="removeKeep(keep.vaultKeepId, vault.id)" data-toggle="modal" title="Remove Keep" :data-target="'#m' + keep.id">
                ❌
              </h5>
            </div>
            <div class=" border-bottom border-dark mb-2">
              <div class="text-center mt-4">
                {{ activeKeep.views }} Views
                {{ activeKeep.keeps }} Keeps
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
                <h5 class="d-flex align-self-center action"
                    v-if="account.id === keep.creatorId"
                    @click="destroy(keep.id, account.id, vault.id)"
                    data-toggle="modal"
                    :data-target="'#m' + keep.id"
                    title="Delete Keep"
                >
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
import { computed, onMounted, reactive } from '@vue/runtime-core'
import { AppState } from '../AppState'
import { keepsService } from '../services/KeepsService'
import { profileService } from '../services/ProfileService'
import Pop from '../utils/Notifier'
import { useRoute } from 'vue-router'
import { vaultKeepService } from '../services/VaultKeepService'
import { vaultsService } from '../services/VaultsService'
export default {
  props: {
    keep: {
      type: Object,
      required: true
    }
  },
  setup() {
    const route = useRoute()
    const state = reactive({
      editedKeep: {}
    })
    onMounted(async() => {
      if (route.params.vaultId !== undefined) {
        AppState.canDelete = true
      } else {
        AppState.canDelete = false
      }
    })
    return {
      state,
      account: computed(() => AppState.account),
      activeKeep: computed(() => AppState.activeKeep),
      vaults: computed(() => AppState.profileVaults),
      vault: computed(() => AppState.activeVault),
      canDelete: computed(() => AppState.canDelete),
      async destroy(keepId, userId, vaultId) {
        try {
          if (await Pop.confirm()) {
            await keepsService.Delete(keepId)
            await keepsService.getAll()
            await profileService.getKeepsByProfile(userId)
            Pop.toast('That keep has been Deleted')
            if (vaultId !== undefined) {
              await vaultsService.getKeeps(vaultId)
            }
          }
        } catch (error) {
          Pop.toast(error, 'error')
        }
      },
      async removeKeep(vaultkeepId, vaultId) {
        try {
          if (await Pop.confirm()) {
            await vaultKeepService.delete(vaultkeepId)
            Pop.toast('That keep has been Removed')
            await vaultsService.getKeeps(vaultId)
          }
        } catch (error) {
          Pop.toast(error, 'error')
        }
      },
      async getVaults(userId) {
        try {
          await profileService.getVaultsByProfile(userId)
        } catch (error) {
          Pop.error(error)
        }
      },
      async getKeepId(keepId) {
        try {
          await keepsService.getById(keepId)
        } catch (error) {
          Pop.toast(error)
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
