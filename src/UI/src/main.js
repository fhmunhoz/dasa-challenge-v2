import Vue from "vue";
import App from "./App.vue";
import VueResource from "vue-resource";
import VueCurrencyFilter from "vue-currency-filter";
import ToggleButton from 'vue-js-toggle-button'

import { library } from "@fortawesome/fontawesome-svg-core";
import {
  faSearch,
  faCartArrowDown,
  faExclamationCircle,
  faCog
} from "@fortawesome/free-solid-svg-icons";
import { FontAwesomeIcon } from "@fortawesome/vue-fontawesome";

import "bootstrap";
import "bootstrap/dist/css/bootstrap.min.css";

library.add(faSearch);
library.add(faCartArrowDown);
library.add(faExclamationCircle);
library.add(faCog);

Vue.component("font-awesome-icon", FontAwesomeIcon);

Vue.config.productionTip = false;
Vue.use(VueResource);
Vue.use(ToggleButton)

Vue.use(VueCurrencyFilter, {
  symbol: "R$",
  thousandsSeparator: ".",
  fractionCount: 2,
  fractionSeparator: ",",
  symbolPosition: "front",
  symbolSpacing: true
});

new Vue({
  render: h => h(App)
}).$mount("#app");
