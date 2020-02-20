<template lang="html">
  <section class="modalAdmin">
    <h5>
      Configurações de webscraping
    </h5>
    <hr />
    <div
      class="list-group"
      v-for="(item, index) in scrapBackGroundWorkers"
      :key="index"
    >      
      <div class="row">
        <div class="col-md-6">
          {{ item.nomeSite }}
        </div>
        <div class="col-md-6">
          <toggle-button @change="ativaDesativaWebScrapSite(item)" width=80 v-model="item.ativo" :labels="{checked: 'Ativo', unchecked: 'Inativo'}"/>  
        </div>
      </div>             
    </div>
    <hr />
    <div class="list-group">
      <div class="row">
        <div class="col-md-12">        
          <h6>
            Automatização (todos os dias 01:00 AM)
          </h6>
        </div>
      </div>
      <div class="row">
        <div class="col-md-6">
          <button class="btn btn-success" @click="ativaDesativaBackGroundWorker(true)">Iniciar</button>
        </div>
        <div class="col-md-6">
          <button class="btn btn-danger" @click="ativaDesativaBackGroundWorker(false)">Parar</button>
        </div>
      </div> 
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
      this.carregarDadosBackGroundWorker();
    },
    methods: {
      carregarDadosBackGroundWorker(){
        
        this.$http
        .get("https://localhost:5001/api/BackGroundWorker/")
        .then(res => res.json())
        .then(scrapBackGroundWorkers => {
          this.scrapBackGroundWorkers = scrapBackGroundWorkers;
        });

      },
      ativaDesativaWebScrapSite(registro){
        
        console.log(registro);
        let urlPost = 'https://localhost:5001/api/BackGroundWorker/'        

        if(registro.ativo){
          urlPost = urlPost + 'ativarWebScrapSite';
        }
        else{
          urlPost = urlPost + 'desativarWebScrapSite';
        }

        this.$http
        .patch(urlPost, registro)
        .then(res => res.json())
        .then(() => {

          // if(!registro.ativo){
          //     alert('Web scraping do site ' + registro.nomeSite  + ' desativado.');
          //   }
          // else{
          //     alert('Web scraping do site ' + registro.nomeSite  + ' ativado.');
          //   }

            this.carregarDadosBackGroundWorker();

        });

      },
      ativaDesativaBackGroundWorker(ativa){
        
        let iniciarImediatamente = false;

        iniciarImediatamente = confirm('Deseja iniciar o scraping imediatamente?');

        this.$http
        .get("https://localhost:5001/api/BackGroundWorker/ativarBackgroudWorker/" + ativa + '/' + iniciarImediatamente)
        .then(res => res.json())
        .then(() => {
          
        });

      }
    }
}
</script>

<style scoped>
.modalAdmin {
  /* width: 350px; */
  /* border: 2px solid rgb(224, 219, 219); */
  background-color: #e3f2fd;
  /* border-radius: 25px; */
  padding: 25px;
}

.botaoAtivaDesativa {
  max-width: 120px;
  min-width: 120px;
}
</style>