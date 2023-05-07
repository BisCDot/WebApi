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
      <button class="btn btn-primary me-md-2" type="button" @click="()=>{modalShow = true; id = 0}">Thêm sản
        phẩm
      </button>
    </div>
    <Transition >
      <EditProduct @save="addProduct" :id="id" v-if="modalShow" @close="closeForm()"></EditProduct>
    </Transition>
  </div>
</template>

<script>

export default {
  name: "ListCource.vue",
  data() {
    return {
      cource: [],
      ValueInputTitle: "",
      modalShow: false,

      Product: {
        Id: "",
        Title: "",
        Description: "",
        Price: 0,
        Image: "",
        categoriesId: ""
      },
      categories: null,
      addProductShow: false,
      editShow: false,
      id: 0
    }
  },
  async created() {
    await this.getList();
  },
  methods: {
    closeForm(){
      this.modalShow = false
    },
    async getList() {
      let ip = await this.$axios.$get('/api/Cource/GetAll');
      this.cource = ip.result;
    },
    async deleteOk(id) {
      await this.$axios.$delete(`/api/Cource/Delete/?Id=${id}`);
      await this.getList();
    },

    async editProduct(id) {
      this.id= id
      this.modalShow = true
    },

    async addProduct(Product) {
      console.log(Product)
      if (Product != null) {
        await this.$axios.post("/api/Cource/Add", {
          title: Product.title,
          description: Product.description,
          image: Product.image,
          price: Product.price,
          categoryId: Product.categoryId
        })
        this.addProductShow = !this.addProductShow;
        await this.getList();
      }
    },
    async SaveProduct() {
      await this.$axios.post('/api/Cource/Save', {
        id: this.updateCource.Id,
        title: this.updateCource.Title,
        description: this.updateCource.Description,
        image: this.updateCource.Image,
        price: this.updateCource.Price,
        categoryId: this.updateCource.categoriesId
      });
      this.editShow = false;
      await this.getList();
    },
  }
}
</script>

<style scoped>
.image-list {
  height: 50px;
  width: 50px;
}
</style>
