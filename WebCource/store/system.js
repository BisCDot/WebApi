import apiUrl from '@/constants/api'
export default {
  state: () => ({
    appSettings: {
      configs: {}
    },
    sOraganizations: null,
    sOraganizationsJoin: null,
    sOraganizationsSelect: [],
    userPermissions: null,
    currentCookieUser: null,
    userPermissionByObject: null,
  }),
  mutations: {
    INIT_SYSTEM: (state, data) => {
      state.appSettings = Object.assign(state.appSettings, data);
    },
    setDomain(state, domain) {
      if (domain.endsWith('/')) { domain = domain.slice(0, -1); }
      state.appSettings.domain = domain;
    },
    SET_LIST : (state,data) => {
        state.sOraganizationsSelect = data
    }
  },
  actions: {
    init({ state, commit }, data) {
      return new Promise((resolve, reject) => {
        return this.$axios.get(apiUrl.SYSTEM_INIT_DATA, { params: { ...data, blockId: this.$config.blockId } }).then(res => {
          commit('INIT_SYSTEM', res.result);
          if (data) { commit('setDomain', data.domain || ''); }
          return resolve(res.result);
        }).catch(err => {
          commit('INIT_SYSTEM', {});
          return reject(err);
        });
      })
    },
    getList({commit},data) {
      return new Promise((resolve, reject) => {
        return this.$axios
          .get(apiUrl.ORGANIZATION_GET_LIST_COURCE)
          .then((response) => {
            commit('SET_LIST', response.data.result);
            return resolve(response.data.result)
          })
        .catch((err) => {
          commit('SET_LIST', null);
          return reject(err)
        })
      })
    }
  },
  getters: {
    sOraganizationsSelect: state => state.sOraganizationsSelect,
    userPermissionByObject: state => state.userPermissionByObject,
  }
}
