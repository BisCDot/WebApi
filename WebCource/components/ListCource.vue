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
        <th scope="col"></th>
      </tr>
      </thead>
      <tr v-for="item in cource">
        <th scope="row"></th>
        <td>{{ item.id }}</td>
        <td>{{ item.title }}</td>
        <td>{{ item.description }}</td>
        <td><img class="image-list" :src="item.image"></td>
        <td>{{ item.price }}</td>
        <td>
          <div class="d-grid gap-2 d-md-block">
            <!-- Button trigger modal -->
            <button type="button" class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#exampleModal"
                    @click="deleteOk(item.id)">
              X
            </button>
          </div>
        </td>
        <td>
          <button type="button" class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#exampleModal"
                  @click="editProduct(item.id)">
            Sửa
          </button>
        </td>
      </tr>
    </table>
    <div class="d-grid gap-2 d-md-flex justify-content-md-end">
      <button class="btn btn-primary me-md-2" type="button" @click="()=>{modalShow = true; id = 0; showIdEditProduct = false; TitleEditProduct= 'Thêm sản phẩm'}">Thêm sản
        phẩm
      </button>
    </div>
    <Transition >
      <EditProduct @save="saveEdit" :title="TitleEditProduct" :id="id" :show-id-input="showIdEditProduct" v-if="modalShow" @closeModal="modalShow = false"></EditProduct>
    </Transition>
  </div>
</template>

<script>

export default {
  name: "ListCource.vue",
  data() {
    return {
      showIdEditProduct : false,
      modalShow: false,
      categories: null,
      id: 0,
      TitleEditProduct : ""
    }
  },
  async created() {
    await this.getList();
  },
  methods: {
     async getList() {
       await this.$store.dispatch('product/getList')
    },
    async deleteOk(id) {
      await this.$store.dispatch('product/delete',id)
      await this.getList();
    },
    async saveEdit(){
      await this.getList();
      this.modalShow = false
    },
    editProduct(id) {
      this.TitleEditProduct = 'Sửa sản phẩm'
      this.showIdEditProduct = true;
      this.id= id;
      this.modalShow = true;
    },
  },
  computed : {
      cource(){
        return this.$store.getters['product/listCourse']
      }
  }
}
</script>

<style scoped>
.image-list {
  height: 50px;
  width: 50px;
}
</style>
