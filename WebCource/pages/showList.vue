<template>
  <div class="list-product">
    <section class="shop">
      <h2 class="section-title">Danh Sách Sản Phẩm</h2>
      <div class="shop-content">
        <div class="product-box" v-for="product in products.result">
          <img :src="product.image" alt="" class="product-img" >
          <h2 class="product-title">{{ product.title }} </h2>
          <span class="price">{{ product.price }}</span>
          <div class="box-icon-shoping">
          </div>
        </div>
      </div>
    </section>
  </div>
</template>

<script>
export default {
  name: "showList.vue",
  layout : "default",
  data(){
    return {
    }
  },
  head() {
    return {
      title: "danh sách khóa học",
      meta: [
        {
          hid: "Description",
          name: "Description",
          content: "khóa học miễn phí",
        },
      ],
      link: [{ rel: 'icon', type: 'image/x-icon', href: '/faviconttest.ico' }]
    };
  },
  async asyncData({store}){
     await this.$store.dispatch('product/getAll')
    const products = this.$store.getters['product/listCourse']
    return {products}
  },
  // async fetch(){
  //   this.products = await fetch('http://localhost:5555/api/Cource/GetAll').then(res => res.json())
  //   console.log(this.products);
  // },
  fetchOnServer : true,
  computed: {
    listCourse() {
      return this.$store.getters['product/listCourse']
    }
  }

}
</script>

<style scoped>
.list-product{

}
.section-title {
  font-size: 1.5rem;
  font-weight: 600;
  text-align: center;
}

.shop {
  margin-top: 2rem;
  margin-bottom: 2rem;
}

.shop-content {
  width: 1038px;
  margin-left: auto;
  margin-right: auto;
  display: grid;
  grid-template-columns: repeat(auto-fit, minmax(auto, 330px));
  gap: 1.5rem;
}

.product-box {
  position: relative;

}

.product-box:hover {
  padding: 10px;
  border: 1px solid #171427;
  transition: 0.4s;
  border-radius: 10px;
}

.product-box .product-img {
  margin-left: auto;
  margin-right: auto;
  width: 50%;
  height: 216px;
  margin-bottom: 0.5rem;
  border-radius: 10px;
  display: block;

}

.price {
  font-weight: 500;
}

.box-icon-shoping {
  position: absolute;
  font-size: 1.8rem;
  cursor: pointer;
  bottom: 0;
  right: 0;
  background: #171427;
  padding: 5px;
  border-radius: 10px;
}

.box-icon-shoping:hover {
  background: hsl(249, 32%, 17%);
}

.product-title {
  font-size: 1.1rem;
  font-weight: 600;
  text-transform: uppercase;
  margin-bottom: 0.5rem;
}
</style>
