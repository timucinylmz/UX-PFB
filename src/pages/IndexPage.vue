<template>
  <q-page>
    <div class="q-pa-md" style="max-width: 500px">
      <q-form class="q-gutter-md">
        <q-input v-model.number="seed" type="number" maxlength="16" outlined></q-input>
        <q-btn @click="calculateHex" color="primary">Calculate</q-btn>
        <q-btn @click="clearAll" color="orange">Clear</q-btn>

        <transition
          appear
          enter-active-class="animated fadeInUp"
          leave-active-class="animated fadeOutDown"
          v-show="showCalculateResult"
        >
          <div>
            namespaceId: <b> {{ nsId }}</b>
            <br />
            Hex-encoded message: <b>{{ message }}</b>
            <br />
            <br />

            <q-input v-model.number="gasLimit" outlined hint="Gas Limit"></q-input>
            <br />
            <q-input v-model.number="fee" outlined hint="Fee"></q-input>
            <br />
            <q-btn @click="makeTransaction" color="primary">Run</q-btn>
          </div>
        </transition>

        <transition
          appear
          enter-active-class="animated fadeInUp"
          leave-active-class="animated fadeOutDown"
          v-show="showTransactionResult"
        >
          <div class="q-pa-xs" style="max-width: 500px">
            <div class="row q-gutter-sm">
              <div>Height:</div>
              <div>
                <b> {{ txResult.height }}</b>
              </div>
              <q-btn
                dense
                @click="displayHeightInfo"
                icon="info"
                size="sm"
                color="primary"
              >
                <q-tooltip>Details</q-tooltip>
              </q-btn>
            </div>
            <br />
            <div class="row q-gutter-sm">
              <div>Txhash:</div>
              <div>
                <b>{{ txResult.txhash }} </b>
              </div>

              <q-btn
                dense
                @click="displayTransaction"
                icon="info"
                size="sm"
                color="primary"
              >
                <q-tooltip>Details</q-tooltip>
              </q-btn>
            </div>

            <br />
            <br />
          </div>
        </transition>
      </q-form>
    </div>
  </q-page>
</template>

<script>
import { defineComponent, ref } from "vue";

var self;

export default defineComponent({
  name: "IndexPage",
  created() {
    self = this;
  },
  setup() {
    const seed = ref(null);
    const gasLimit = ref(80000);
    const fee = ref(2000);
    const showCalculateResult = ref(false);
    const showTransactionResult = ref(false);
    const nsId = ref(null);
    const message = ref(null);
    const txResult = ref({ height: 0, txhash: "" });

    const displayTransaction = () => {
      window.open(
        `https://testnet.mintscan.io/celestia-incentivized-testnet/txs/${txResult.value.txhash}?height=${txResult.value.height}`
      );
    };

    const displayHeightInfo = () => {
      window.open(
        `https://testnet.mintscan.io/celestia-incentivized-testnet/blocks/id/${txResult.value.height}`
      );
    };

    const makeTransaction = async () => {
      showTransactionResult.value = false;

      try {
        let result = await self.$http.post(`api/blobs/tx`, {
          namespace_id: nsId.value,
          data: message.value,
          gas_limit: gasLimit.value,
          fee: fee.value,
        });

        txResult.value = { height: 0, txhash: "" };

        if (result.data.success) {
          txResult.value = result.data.data;
          showTransactionResult.value = true;
        } else {
          self.$q.notify({ type: "negative", message: result.data.message });
        }
      } catch (error) {
        self.$q.notify({ type: "negative", message: error });
      }
    };

    const clearAll = () => {
      showCalculateResult.value = false;
      showTransactionResult.value = false;
    };

    const calculateHex = async () => {
      showCalculateResult.value = false;
      showTransactionResult.value = false;

      let result = await self.$http.get(`api/blobs/calculate?seed=${seed.value}`);

      if (result.data.success) {
        let data = result.data.data;

        nsId.value = data.nsId;
        message.value = data.msg;

        showCalculateResult.value = true;

        self.$q.notify({
          type: "positive",
          html: true,
          message: `namespaceId: ${data.nsId} <br/> Message: ${data.msg}`,
        });
      } else {
        self.$q.notify({
          type: "negative",
          message: result.data.message,
        });
      }
    };

    return {
      gasLimit,
      fee,
      seed,
      calculateHex,
      showCalculateResult,
      showTransactionResult,
      nsId,
      message,
      makeTransaction,
      txResult,
      clearAll,
      displayTransaction,
      displayHeightInfo,
    };
  },
});
</script>
