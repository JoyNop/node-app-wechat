const express = require('express');
const mongoose = require('mongoose');
const bodyParser = require('body-parser');
const passport = require('passport');
const app = express();

// 引入uers.js
const users = require('./routers/api/user');
const profile = require('./routers/api/profile');

// DB config
const db = require('./config/keys').mongoURI;

// 使用body-parser中间件
app.use(bodyParser.urlencoded({ extended: false }));
app.use(bodyParser.json());

// passport初始化
app.use(passport.initialize());
require('./config/passport')(passport);

// Connect to Mongodb
mongoose
  .connect(
    db,
    { useNewUrlParser: true }
  )
  .then(() => console.log('MongoDB Connected'))
  .catch(err => console.log(err));


//   app.use('*', function(req, res, next) {
//     res.header("Access-Control-Allow-Origin", "*");
//     res.header("Access-Control-Allow-Headers", "X-Requested-With");
//     res.header("Access-Control-Allow-Methods","PUT,POST,GET,DELETE,OPTIONS");
//     res.header("X-Powered-By",' 3.2.1')
//     res.header("Content-Type", "application/json;charset=utf-8");
//     next();
// });

 
// app.get('/', (req, res) => {
//     res.send('hello word');
// })

// 使用routers
app.use('/api/users', users);
app.use('/api/profile', profile);

const port = process.env.PORT || 5000;

app.listen(port, () => {
  console.log(`Server is running on port ${port}`);
});
