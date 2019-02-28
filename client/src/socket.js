export default {
  //websocket实例
  //保证socket只有一个socket实例，单例
  // const ws = new WebSocket("ws://localhost:3000")
  ws: null,//websocket实例

  //config 传ID用于链接websocket
  //onMessage 当客户端接收到消息是回调触发
  //错误回调
  init(config, onMessage, onError) {
    if (this.ws) this.ws.close();
    //实例化socket对象
    this.ws = new WebSocket(`ws://localhost:3000/${config.user.id}`)
    //客户端接收消息
    this.ws.onmessage = event => {
      let message = JSON.parse(event.data);
      onMessage && onMessage(message);//接收到消息触发的回调
    };

    //出错
    this.ws.onerror = error => {
      onError && onError(error)

    };
  },
  send(msgObj) {//发送消息时触发
    this.ws.send(JSON.stringify(msgObj))
  }
}