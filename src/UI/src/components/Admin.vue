<template lang="html">
  <section>
    <div
      class="list-group"
      v-for="(item, index) in scrapBackGroundWorkers"
      :key="index"
    >
      {{ item.nomeSite }}

      <button
        type="button"
        class="list-group-item list-group-item-action active"
        @click="ativaDesativaBackGroudWorker(item)"
      >
        <span v-if="item.ativo">Desativar webscrap</span>
        <span v-else>Ativar webscrap</span>
      </button>
    </div>
  </section>
</template>

<script lang="js">

  export default  {
    name: 'Admin',
    data () {
      return {
          scrapBackGroundWorkers: []
      }
    },
    created() {
      this.$http
        .get("https://localhost:5001/api/BackGroundWorker/")
        .then(res => res.json())
        .then(scrapBackGroundWorkers => {
          this.scrapBackGroundWorkers = scrapBackGroundWorkers;
        });
    },
    methods: {
      ativaDesativaBackGroudWorker(registro){

        let urlPost = 'https://localhost:5001/api/BackGroundWorker/'

        if(registro.ativo){
          urlPost = urlPost + 'desativar';
        }
        else{
          urlPost = urlPost + 'ativar';
        }

        this.$http
        .patch(urlPost, registro)
        .then(res => res.json())
        .then(() => {

          if(registro.ativo){
              alert('Web scraping do site ' + registro.nomeSite  + ' desativado.');
            }
          else{
              alert('Web scraping do site ' + registro.nomeSite  + ' ativado.');
            }

        });

      }
    }
}
</script>
