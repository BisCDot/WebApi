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
      <tr v-for="item in Cource">
        <th scope="row"></th>
        <td>{{item.id}}</td>
        <td>{{item.title}}</td>
        <td>{{item.description}}</td>
        <td><img class="image-list" :src="item.image"></td>
        <td>{{item.price}}</td>
        <td>
          <div class="d-grid gap-2 d-md-block">
            <!-- Button trigger modal -->
            <button type="button" class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#exampleModal"  @click="DeleteOk(item.id)">
              X
            </button>
<!--            <b-modal v-model="modalShow" title="Xóa" @ok="DeleteOk">Bạn có chắc muốn xóa item này không ?</b-modal>-->
          </div>
        </td>
        <td>
          <button type="button" class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#exampleModal"  @click="Test(item)">
              Sửa
          </button>
          <b-modal
            v-model="modalShow"
            id="modal-prevent-closing"
            ref="modal"
            title="Sửa sản phẩm"
            @show="resetModal"
            @hidden="resetModal"
            @ok="SaveProduct"
          >
            <form ref="form" @submit.stop.prevent="handleSubmit">
              <b-form-group
                label="Id"
                label-for="name-input"
                invalid-feedback="Name is required"
                :state="item.id"
              >
                <b-form-input
                  id="name-input"
                  :state="item.id"
                  v-model="updateCource.Id"
                  v-model:value="updateCource.Id"
                  required
                ></b-form-input>
              </b-form-group>
              <b-form-group
                label="Title"
                label-for="name-input"
                invalid-feedback="Title is required"

              >
                <b-form-input
                  id="name-input"
                  v-model="updateCource.Title"
                  :state="item.id"
                  v-model:value="updateCource.Title"
                  required
                ></b-form-input>
              </b-form-group>
              <b-form-group
                label="Description"
                label-for="name-input"
                invalid-feedback="Description is required"

              >
                <b-form-input
                  id="name-input"
                  v-model="updateCource.Description"
                  :state="item.id"
                  v-model:value="updateCource.Description"
                  required
                ></b-form-input>
              </b-form-group>
              <b-form-group
                label="Link image"
                label-for="name-input"
                invalid-feedback="image is required"

              >
                <b-form-input
                  id="name-input"
                  v-model="updateCource.Image"
                  :state="item.id"
                  v-model:value="updateCource.Image"
                  required
                ></b-form-input>
              </b-form-group>
              <b-form-group
                label="Price"
                label-for="name-input"
                invalid-feedback="Price is required"

              >
                <b-form-input
                  id="name-input"
                  v-model="updateCource.Price"
                  :state="item.id"
                  v-model:value="updateCource.Price"
                  required
                ></b-form-input>
              </b-form-group>
              <select class="form-select form-select-sm" aria-label=".form-select-sm example" v-model="updateCource.CategoriesId">
                <option selected >Chọn danh mục</option>
                <option v-for="item in Categories" :value="item.id">{{item.name}}</option>
              </select>
            </form>
          </b-modal>
        </td>
      </tr>
    </table>

  </div>

</template>

<script>


export default {

  name: "ListCource.vue",
  data(){
    return{
        Cource : [],
        modalShow : false,
        updateCource : {
          Id : "",
          Title : "",
          Description : "",
          Price : "",
          Image : "",
          CategoriesId : 0
        },
        Categories : null

    }
  },
  async created() {
      await this.getList();
      await this.getCategory();
  },
  methods: {
    async getList(){
      var ip = await this.$axios.$get('/api/Cource/GetAll')
      this.Cource = ip.result
    },
    async DeleteOk(id){
      await this.$axios.$delete(`/api/Cource/Delete/?Id=${id}`)
    },
    async getCategory(){
      var result = await this.$axios.$get('/api/Category/GetAll');
      this.Categories = result.result
    },
    Test(item){
      this.modalShow = !this.modalShow;
      this.updateCource.Title = item.title;
      this.updateCource.Id = item.id;
      this.updateCource.Description = item.description;
      this.updateCource.Image = item.image;
      this.updateCource.Price = item.price;
    },
    async SaveProduct(){
        await this.$axios.post('/api/Cource/Save',{
            id : this.updateCource.Id,
            title : this.updateCource.Title,
            description : this.updateCource.Description,
            image : this.updateCource.Image,
            price : this.updateCource.Price,
            categorysId : this.updateCource.CategoriesId
       })
    }
  }
}
</script>

<style scoped>
.image-list{
  height: 50px;
  width: 50px;
}
</style>
