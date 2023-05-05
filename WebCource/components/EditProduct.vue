<template>
  <div class="modal-vue">
    <div class="overlay" @click="CloseModal"></div>
    <div class="modal">
      <button class="close" @click="CloseModal">x</button>
      <h3>{{Title}}</h3>
      <div class="main-product-add">
        <div v-if="showId">
          <b-form-group
            label="Id"
            label-for="name-input"
            invalid-feedback="Name is required"
            :state="product.id"
          >
            <b-form-input
              id="name-input"
              :state="product.id"
              v-model="product.Id"
              v-model:value="product.Id"
            ></b-form-input>
          </b-form-group>
        </div>
        <b-form-group
          label="Title"
          label-for="name-input"
          invalid-feedback="Title is required"
        >
          <b-form-input
            id="name-input"
            v-model="product.title"
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
            v-model="product.Description"
            :value="product.Description"
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
            v-model="product.Image"
            v-model:value="product.Image"
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
            v-model="product.Price"
            v-model:value="product.Price"
            required
          ></b-form-input>
        </b-form-group>
          <select class="form-select form-select-sm" aria-label=".form-select-sm example"  v-model="product.categoryId" >
            <option selected>Chọn danh mục</option>
            <option v-for="item in Categories" :value="item.id">
              {{item.name}}
            </option>
          </select>
          <div class="d-grid gap-2 d-md-flex justify-content-md-end">
            <button class="btn btn-primary me-md-2" type="button" @click="Save(product)">Ok</button>
          </div>
      </div>
    </div>
  </div>
</template>
<script>
export default {
  name: "Edit.vue",
  props: {
    id: {
      type: Number,
      default: 0
    },
    title: {
      type: String,
      default: ''
    }
  },
  emits: ['Save'],
  data() {
    return {
      ValueInputTitle: "",
      product: {},
      Categories: null,
      id: 0
    }
  },
  async created() {
    await this.getDetail()
    await this.getCategory()
  },
  methods : {
    CloseModal(){
      this.$emit('close')
    },
    Save(product){
      this.$emit('Save',product)
    },
    async getCategory() {
      var result = await this.$axios.$get('/api/Category/GetAll');
      this.Categories = result.result
    },
    async getDetail(){
      if (this.id > 0) {
        var value = await this.$axios.$get(`/api/Cource/GetById/?Id=${this.id}`);
        this.product = value.result;
      }else{
        this.product = {}
      }
    }
  }
}
</script>

<style scoped>
.modal{
  display: block;
}
.modal-vue .overlay {
  position: fixed;
  z-index: 998;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  background-color: rgba(0, 0, 0, .5);
}

.modal-vue .modal {
  position: relative;
  width: 600px;
  height: 600px;
  z-index: 999;
  margin: 0 auto;
  padding: 20px 30px;
  flex-direction: column;
  background-color: #fff;
  border-radius: 0.3rem;
  background-clip: padding-box;
  border: 1px solid rgba(0, 0, 0, 0.2);
}

.modal-vue .close{
  position: absolute;
  top: 10px;
  right: 10px;
}
</style>
