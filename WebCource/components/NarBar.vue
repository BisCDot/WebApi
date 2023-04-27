<template>
  <nav class="navbar navbar-expand-lg bg-body-tertiary">
    <div class="container-fluid">
      <a class="navbar-brand"><nuxt-link to="/">Home</nuxt-link></a>
      <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarNav" aria-controls="navbarNav" aria-expanded="false" aria-label="Toggle navigation">
        <span class="navbar-toggler-icon"></span>
      </button>
      <div class="collapse navbar-collapse" id="navbarNav">
        <ul class="navbar-nav">
          <li class="nav-item">
            <a class="nav-link" >
              <nuxt-link to="/productadd">Thêm sản phẩm</nuxt-link>
            </a>
          </li>
          <li class="nav-item">
            <a class="nav-link" >
              <nuxt-link to="/categoryadd">Quản lý danh mục</nuxt-link>
            </a>
          </li>
        </ul>
      </div>
    </div>
  </nav>
</template>

<script>
export default {
  name: "NarBar"
}
</script>

<style scoped>
.navbar{
  border-bottom-style: solid;
  border-bottom-color: dimgray;
}
</style>
