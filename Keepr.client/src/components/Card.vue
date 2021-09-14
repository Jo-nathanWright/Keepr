<template>
  <div class="card" data-toggle="modal" :data-target="'#m' + keep.id">
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
              <div class="d-flex justify-content-between">
                <div class="btn-group">
                  <button type="button" class="btn btn-outline-success dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                    Add to Vault
                  </button>
                  <div class="dropdown-menu">
                    <a class="dropdown-item">Future Vault</a>
                  </div>
                </div>
                <h5 class="d-flex align-self-center action" v-if="account.id === keep.creatorId">
                  🗑
                </h5>
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
import { computed } from '@vue/runtime-core'
import { AppState } from '../AppState'
export default {
  props: {
    keep: {
      type: Object,
      required: true
    }
  },
  setup() {
    return {
      account: computed(() => AppState.account)
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
