<template>
  <div class="container-fluid mt-5">
    <div class="card-columns">
      <!-- Bootstrap Cards show how to use this -->
      <span @click="update">
        <KeepCard v-for="k in keeps" :key="k.id" :keep="k" />
      </span>
    </div>
  </div>
</template>

<script>
import { computed, onMounted } from '@vue/runtime-core'
import Pop from '../utils/Notifier'
import { keepsService } from '../services/KeepsService'
import { AppState } from '../AppState'
export default {
  name: 'Home',
  setup() {
    onMounted(async() => {
      try {
        await keepsService.getAll()
      } catch (error) {
        Pop.toast('Something went wrong')
      }
    })
    return {
      keeps: computed(() => AppState.keeps),
      async update() {
        try {
          await keepsService.getAll()
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
  height: 50%;
}
@media only screen and (min-width: 720px){
  .card-columns{
    column-count: 4;
  }
}
</style>
