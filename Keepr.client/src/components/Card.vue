<template>
  <div class="card" data-toggle="modal" :data-target="'#m' + keep.id">
    <img class="card-img-top" :src="keep.img" alt="Card image cap">
    <div class="card-img-overlay text-light d-flex align-items-end justify-content-between">
      <h5 class="card-title">
        {{ keep.name }} {{ keep.id }}
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
          <div class="col-6 my-2">
            <img class="card-img-top" :src="keep.img" alt="Card image cap">
          </div>
          <div class="col-5">
            <div class=" text-center mt-4">
              {{ keep.views }} Views
              {{ keep.keeps }} Keeps
            </div>
            <div class="text-center mt-4">
              <h3>{{ keep.name }}</h3>
            </div>
            <div class="mt-3">
              <h5>
                {{ keep.description }}
              </h5>
            </div>
            <hr>
            <div class="text-right">
              <img class="rounded-circle image" :src="keep.creator.picture" alt="keep.creator.name">
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  props: {
    keep: {
      type: Object,
      required: true
    }
  },
  setup() {
    return {}
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
</style>
