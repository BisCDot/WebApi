<template>
  <div>
    <table class="table">
      <thead class="table-dark">
        <tr>
          <th scope="col">#</th>
          <th scope="col">Id</th>
          <th scope="col">title</th>
          <th scope="col">Description</th>
          <th scope="col">Image</th>
          <th scope="col">Price</th>
          <th scope="col"></th>
        </tr>
      </thead>
      <tr v-for="item in Cource">
        <th scope="row"></th>
        <td>{{item.id}}</td>
        <td>{{item.title}}</td>
        <td>{{item.description}}</td>
        <td><img class="image-list" :src="item.image"></td>
        <td>{{item.price}}</td>
      </tr>
    </table>

  </div>

</template>

<script>
import { fab } from '@fortawesome/free-brands-svg-icons'


export default {

  name: "ListCource.vue",
  data(){
    return{
        Cource : []
    }
  },
  async created() {
    var ip = await this.$axios.$get('/api/Cource/GetAll')
    this.Cource = ip.result
  }
}
</script>

<style scoped>
.image-list{
  height: 50px;
  width: 50px;
}
</style>
