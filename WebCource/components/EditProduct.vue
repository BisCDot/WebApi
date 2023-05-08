<template>
  <div class="modal-vue">
    <div class="overlay" @click="closeModal"></div>
    <div class="modal">
      <button class="close" @click="closeModal">x</button>
      <h3>{{title}}</h3>
      <div class="main-product-add">
        <b-form-group
          v-if="showIdInput"
          label="Id"
          label-for="name-input"
          invalid-feedback="Id is required"
        >
          <b-form-input
            id="name-input"
            v-model="product.id"
            required
          >
          </b-form-input>
        </b-form-group>
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
            v-model="product.description"
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
            v-model="product.image"
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
            v-model:value="product.price"
            required
          ></b-form-input>
        </b-form-group>
          <select class="form-select form-select-sm" aria-label=".form-select-sm example"  v-model="product.categoryId" >
            <option selected>Chọn danh mục</option>
            <option v-for="item in categories" :value="item.id">
              {{item.name}}
            </option>
          </select>
          <div class="d-grid gap-2 d-md-flex justify-content-md-end">
            <button class="btn btn-primary me-md-2" type="button" @click="save">Ok</button>
          </div>
      </div>
    </div>
  </div>
</template>
<script>
export default {
  name: "EditProduct.vue",
  props: {
    showIdInput : {
      type : Boolean,
      default : false
    },
    id: {
      type: Number,
      default: 0
    },
    title: {
      type: String,
      default: ''
    }
  },
  emits: ['closeModal','save'],
  data() {
    return {
      valueInputTitle: "",
      product: {},
      categories: [],
    }
  },
  async created() {
    await this.getDetail()
    await this.getCategory()
  },
  methods : {
    closeModal(){
      this.$emit('closeModal')
    },
   async save(){
      if (this.id > 0) {
        await this.$axios.post('/api/Cource/Save', {
          id: this.product.id,
          title: this.product.title,
          description: this.product.description,
          image: this.product.image,
          price: this.product.price,
          categoryId: this.product.categoryId
        });
      }else {
        await this.$axios.post("/api/Cource/Add", {
          title: this.product.title,
          description: this.product.description,
          image: this.product.image,
          price: this.product.price,
          categoryId: this.product.categoryId
        })
      }
      this.$emit('save')
    },
    async getCategory() {
      let result = await this.$axios.$get('/api/Category/GetAll');
      this.categories = result.result
    },
    async getDetail(){
      if (this.id > 0) {
        let value = await this.$axios.$get(`/api/Cource/GetById/?Id=${this.id}`);
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
