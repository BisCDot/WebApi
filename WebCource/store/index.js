async function initData(dispatch, context, domain) {

}

export const actions = {
  async nuxtServerInit({ dispatch }, context) {
    if (context.route && context.route.path) {
      if (context.route.path.lastIndexOf('/healthz') === -1) {
        const headers = (context.req && context.req.headers) ? Object.assign({}, context.req.headers) : {}
        const ip = context.req.connection.remoteAddress || context.req.socket.remoteAddress
        console.info('server render: ', context.route.path, headers['user-agent'], ip || '', headers['x-forwarded-for'] || '');
      }
    }
    const domain = context.req.headers.referer || context.req.headers.host;
    await initData(dispatch, context, domain)
  },
  async nuxtClientInit({ dispatch }, context) {
    const domain = document.referrer || window.location.host;
    await initData(dispatch, context, domain)
  }
}

