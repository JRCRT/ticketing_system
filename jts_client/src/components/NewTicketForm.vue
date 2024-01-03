<template>
  <Modal @close="$emit('close')">
    <template v-slot:header>
      <h5 class="modal-title">New</h5>
    </template>
    <template v-slot:content>
      <div class="new_ticket_form_container">
        <figure class="table" style="width: 800px; padding: 10px">
          <table
            class="ck-table-resized"
            style="border: 3px double hsl(0, 0%, 0%)"
          >
            <colgroup>
              <col style="width: 4.07%" />
              <col style="width: 4.61%" />
              <col style="width: 4.27%" />
              <col style="width: 4.27%" />
              <col style="width: 4.27%" />
              <col style="width: 6.57%" />
              <col style="width: 6.27%" />
              <!---->
              <col style="width: 6.27%" />
              <col style="width: 6.27%" />
              <col style="width: 8.27%" />
              <col style="width: 8.27%" />
              <col style="width: 8.27%" />
              <col style="width: 8.27%" />
              <col style="width: 8.5%" />
              <col style="width: 10.27%" />
              <col style="width: 10.28%" />
            </colgroup>
            <tbody>
              <tr>
                <td
                  style="
                    font-weight: bold;
                    height: 18.5pt;
                    width: 96pt;
                    font-size: 8pt;
                    border-bottom: 2px solid hsl(0, 0%, 0%);
                  "
                  colspan="2"
                >
                  Approved Date
                </td>
                <td
                  style="
                    border: 2px solid hsl(0, 0%, 0%);
                    width: 192pt;
                    font-size: 9pt;
                    padding-left: 3px;
                  "
                  colspan="4"
                ></td>
                <td
                  style="
                    font-weight: bold;
                    border-right: 2px solid hsl(0, 0%, 0%);
                    width: 288pt;
                    text-align: center;
                    vertical-align: top;
                    font-size: 14pt;
                  "
                  colspan="6"
                  rowspan="3"
                >
                  APPLICATION (稟議書)
                </td>
                <td
                  style="
                    width: 96pt;
                    font-size: 8pt;
                    font-weight: bold;

                    border-right: 2px solid hsl(0, 0%, 0%);
                    border-bottom: 2px solid hsl(0, 0%, 0%);
                  "
                  colspan="2"
                >
                  Applied Date
                </td>
                <td
                  style="
                    padding-left: 3px;
                    font-size: 9pt;
                    border-bottom: 2px solid hsl(0, 0%, 0%);
                    width: 96pt;
                  "
                  colspan="2"
                ></td>
              </tr>
              <tr>
                <td
                  style="
                    font-weight: bold;
                    height: 30pt;
                    width: 96pt;
                    font-size: 8pt;
                  "
                  colspan="2"
                  rowspan="2"
                >
                  Approved No.
                </td>
                <td
                  style="
                    border-left: 2px solid hsl(0, 0%, 0%);
                    border-right: 2px solid hsl(0, 0%, 0%);
                    width: 192pt;
                    padding: 3px;
                  "
                  colspan="4"
                  rowspan="2"
                >
                  <!--Approved No-->
                </td>

                <td
                  style="
                    width: 96pt;

                    border-right: 2px solid hsl(0, 0%, 0%);
                    font-size: 8pt;
                    font-weight: bold;
                  "
                  colspan="2"
                >
                  Receipt No.
                </td>

                <td
                  style="border-bottom: 2px solid hsl(0, 0%, 0%); width: 96pt"
                  colspan="2"
                >
                  <!--Receipt No-->
                </td>
              </tr>
              <tr>
                <td
                  style="
                    font-weight: bold;

                    border-right: 2px solid hsl(0, 0%, 0%);
                    width: 96pt;
                    font-size: 8pt;
                  "
                  colspan="2"
                >
                  Receipt Date
                </td>
                <td style="width: 96pt" colspan="2">
                  <!--Receipt Date-->
                </td>
              </tr>

              <tr>
                <td
                  style="
                    text-align: center;
                    background-color: hsl(192, 51%, 90%);
                    font-size: 8pt;
                    border-top: 2px solid hsl(0, 0%, 0%);
                    border-left: 2px solid hsl(0, 0%, 0%);
                    border-right: 2px solid hsl(0, 0%, 0%);
                    height: 14.5pt;
                    vertical-align: top;
                    width: 48pt;
                    font-weight: bold;
                  "
                  rowspan="4"
                >
                  <p>決裁者</p>
                  <p>Approved by</p>
                </td>
                <td
                  style="
                    padding-bottom: 5px;
                    font-size: 8pt;
                    border-top: 2px solid hsl(0, 0%, 0%);
                    border-right: 2px solid hsl(0, 0%, 0%);

                    text-align: center;
                    width: 240pt;
                  "
                  colspan="3"
                >
                  <p style="font-weight: bold">JACCS CO., LTD</p>
                  <p style="font-size: 6pt">(BOARD OF DIRECTORS)&nbsp;</p>
                </td>
                <td
                  style="
                    padding-bottom: 5px;
                    font-size: 8pt;
                    border-top: 2px solid hsl(0, 0%, 0%);
                    border-right: 2px solid hsl(0, 0%, 0%);
                    text-align: center;
                    width: 288pt;
                  "
                  colspan="4"
                >
                  <p style="font-weight: bold">JACCS CO., LTD&nbsp;</p>
                  <p style="font-size: 6pt">
                    (INTERNATIONAL BUSINESS DIVISION)
                  </p>
                </td>
                <td
                  style="
                    padding-bottom: 5px;
                    font-size: 8pt;
                    background-color: hsl(192, 51%, 90%);
                    border-right: 2px solid hsl(0, 0%, 0%);
                    border-top: 2px solid hsl(0, 0%, 0%);
                    text-align: center;
                    width: 192pt;
                  "
                  colspan="9"
                >
                  <p style="font-weight: bold">JFP</p>
                  <p style="font-size: 6pt">
                    (JACCS FINANCE PHILIPPINES CORPORATION)
                  </p>
                </td>
              </tr>
              <tr>
                <td
                  style="
                    padding-bottom: 5px;
                    text-align: center;
                    font-weight: bold;
                    font-size: 8pt;
                    border-bottom: 2px solid hsl(0, 0%, 0%);
                    border-right: 2px solid hsl(0, 0%, 0%);
                    border-top: 2px solid hsl(0, 0%, 0%);
                    width: 96pt;
                  "
                  colspan="1"
                >
                  □CEO
                </td>
                <td
                  style="
                    padding-bottom: 5px;
                    text-align: center;
                    font-weight: bold;
                    font-size: 8pt;
                    border-bottom: 2px solid hsl(0, 0%, 0%);
                    border-right: 2px solid hsl(0, 0%, 0%);
                    border-top: 2px solid hsl(0, 0%, 0%);
                    width: 48pt;
                  "
                  colspan="1"
                >
                  □COO
                </td>
                <td
                  style="
                    padding-bottom: 5px;
                    text-align: center;
                    font-weight: bold;
                    font-size: 8pt;
                    border-bottom: 2px solid hsl(0, 0%, 0%);
                    border-right: 2px solid hsl(0, 0%, 0%);
                    border-top: 2px solid hsl(0, 0%, 0%);
                    width: 96pt;
                  "
                  colspan="1"
                >
                  □担当役員
                </td>
                <td
                  style="
                    padding-bottom: 5px;
                    text-align: center;
                    font-weight: bold;
                    font-size: 8pt;
                    border-bottom: 2px solid hsl(0, 0%, 0%);
                    border-right: 2px solid hsl(0, 0%, 0%);
                    border-top: 2px solid hsl(0, 0%, 0%);
                    width: 96pt;
                  "
                  colspan="1"
                >
                  □部長
                </td>
                <td
                  style="
                    padding-bottom: 5px;
                    text-align: center;
                    font-weight: bold;
                    font-size: 8pt;
                    border-bottom: 2px solid hsl(0, 0%, 0%);
                    border-right: 2px solid hsl(0, 0%, 0%);
                    border-top: 2px solid hsl(0, 0%, 0%);
                    width: 96pt;
                  "
                  colspan="1"
                >
                  課長
                </td>
                <td
                  style="
                    padding-bottom: 5px;
                    text-align: center;
                    font-weight: bold;
                    font-size: 8pt;
                    border-bottom: 2px solid hsl(0, 0%, 0%);
                    border-right: 2px solid hsl(0, 0%, 0%);
                    border-top: 2px solid hsl(0, 0%, 0%);
                    width: 48pt;
                  "
                >
                  検印
                </td>
                <td
                  style="
                    padding-bottom: 5px;
                    text-align: center;
                    font-weight: bold;
                    font-size: 8pt;
                    border-bottom: 2px solid hsl(0, 0%, 0%);
                    border-right: 2px solid hsl(0, 0%, 0%);
                    border-top: 2px solid hsl(0, 0%, 0%);
                    width: 48pt;
                  "
                >
                  担当
                </td>
                <td
                  style="
                    padding-bottom: 5px;
                    text-align: center;
                    font-weight: bold;
                    font-size: 8pt;
                    border-bottom: 2px solid hsl(0, 0%, 0%);
                    border-right: 2px solid hsl(0, 0%, 0%);
                    border-top: 2px solid hsl(0, 0%, 0%);
                    width: 96pt;
                  "
                  colspan="2"
                >
                  □President
                </td>
                <td
                  style="
                    padding-bottom: 5px;
                    text-align: center;
                    font-weight: bold;
                    font-size: 8pt;
                    border-bottom: 2px solid hsl(0, 0%, 0%);
                    border-right: 2px solid hsl(0, 0%, 0%);
                    border-top: 2px solid hsl(0, 0%, 0%);
                    width: 40pt;
                  "
                  colspan="2"
                >
                  CFO
                </td>
                <td
                  style="
                    padding-bottom: 5px;
                    text-align: center;
                    font-weight: bold;
                    font-size: 8pt;
                    border-bottom: 2px solid hsl(0, 0%, 0%);
                    border-right: 2px solid hsl(0, 0%, 0%);
                    border-top: 2px solid hsl(0, 0%, 0%);
                    width: 40pt;
                  "
                  colspan="2"
                >
                  EVP
                </td>
                <td
                  style="
                    padding-bottom: 5px;
                    text-align: center;
                    font-weight: bold;
                    font-size: 8pt;
                    border-bottom: 2px solid hsl(0, 0%, 0%);
                    border-right: 2px solid hsl(0, 0%, 0%);
                    border-top: 2px solid hsl(0, 0%, 0%);
                    width: 48pt;
                  "
                  colspan="3"
                >
                  SVP
                </td>
              </tr>

              <tr>
                <!--JACCS CO-->
                <td
                  style="
                    border-bottom: 2px dashed hsl(0, 0%, 0%);
                    border-right: 2px solid hsl(0, 0%, 0%);
                    width: 96pt;
                  "
                  colspan="1"
                >
                  &nbsp;
                </td>
                <td
                  style="
                    border-bottom: 2px dashed hsl(0, 0%, 0%);
                    border-right: 2px solid hsl(0, 0%, 0%);
                    width: 48pt;
                  "
                >
                  &nbsp;
                </td>
                <td
                  style="
                    border-bottom: 2px dashed hsl(0, 0%, 0%);
                    border-right: 2px solid hsl(0, 0%, 0%);
                    width: 96pt;
                  "
                  colspan="1"
                >
                  &nbsp;
                </td>
                <!--JACCS CO-->

                <!--IBD-->
                <td
                  style="
                    border-bottom: 2px dashed hsl(0, 0%, 0%);
                    border-right: 2px solid hsl(0, 0%, 0%);
                    width: 96pt;
                  "
                  colspan="1"
                >
                  &nbsp;
                </td>
                <td
                  style="
                    border-bottom: 2px dashed hsl(0, 0%, 0%);
                    border-right: 2px solid hsl(0, 0%, 0%);
                    width: 96pt;
                  "
                  colspan="1"
                >
                  &nbsp;
                </td>
                <td
                  style="
                    border-bottom: 2px dashed hsl(0, 0%, 0%);
                    border-right: 2px solid hsl(0, 0%, 0%);
                    width: 48pt;
                  "
                >
                  &nbsp;
                </td>
                <!--IBD-->

                <td
                  style="
                    border-bottom: 2px dashed hsl(0, 0%, 0%);
                    border-right: 2px solid hsl(0, 0%, 0%);
                    width: 48pt;
                  "
                >
                  &nbsp;
                </td>
                <td
                  style="
                    border-bottom: 2px dashed hsl(0, 0%, 0%);
                    border-right: 2px solid hsl(0, 0%, 0%);
                    width: 96pt;
                  "
                  colspan="2"
                ></td>
                <td
                  style="
                    border-bottom: 2px dashed hsl(0, 0%, 0%);
                    border-right: 2px solid hsl(0, 0%, 0%);
                    width: 96pt;
                  "
                  colspan="2"
                ></td>
                <td
                  style="
                    border-bottom: 2px dashed hsl(0, 0%, 0%);
                    border-right: 2px solid hsl(0, 0%, 0%);
                    width: 48pt;
                  "
                  colspan="2"
                ></td>
                <td
                  style="border-bottom: 2px dashed hsl(0, 0%, 0%); width: 48pt"
                  colspan="3"
                ></td>
              </tr>

              <tr>
                <td
                  style="border-right: 2px solid hsl(0, 0%, 0%); width: 96pt"
                  colspan="1"
                >
                  &nbsp;
                </td>
                <td style="border-right: 2px solid hsl(0, 0%, 0%); width: 48pt">
                  &nbsp;
                </td>
                <td
                  style="border-right: 2px solid hsl(0, 0%, 0%); width: 96pt"
                  colspan="1"
                >
                  &nbsp;
                </td>
                <td
                  style="border-right: 2px solid hsl(0, 0%, 0%); width: 96pt"
                  colspan="1"
                >
                  &nbsp;
                </td>
                <td
                  style="border-right: 2px solid hsl(0, 0%, 0%); width: 96pt"
                  colspan="1"
                >
                  &nbsp;
                </td>
                <td style="border-right: 2px solid hsl(0, 0%, 0%); width: 48pt">
                  &nbsp;
                </td>
                <td style="border-right: 2px solid hsl(0, 0%, 0%); width: 48pt">
                  &nbsp;
                </td>
                <td
                  style="
                    border-right: 2px solid hsl(0, 0%, 0%);
                    width: 96pt;
                    padding: 3px;
                  "
                  colspan="2"
                >
                  <VueMultiselect
                    v-model="selectedPresident"
                    :options="getApprover('President')"
                    label="ext_name"
                    :show-labels="false"
                  />
                </td>
                <td
                  style="
                    border-right: 2px solid hsl(0, 0%, 0%);
                    width: 96pt;
                    font-size: 10pt;
                    padding: 3px;
                  "
                  colspan="2"
                >
                  <VueMultiselect
                    v-model="selectedCFO"
                    :options="getApprover('CFO')"
                    label="ext_name"
                    :show-labels="false"
                  />
                </td>
                <td
                  style="
                    border-right: 2px solid hsl(0, 0%, 0%);
                    width: 48pt;
                    font-size: 10pt;
                    padding: 3px;
                  "
                  colspan="2"
                >
                  <VueMultiselect
                    v-model="selectedEVP"
                    :options="getApprover('EVP')"
                    label="ext_name"
                    :show-labels="false"
                  />
                </td>
                <td
                  style="width: 48pt; font-size: 10pt; padding: 3px"
                  colspan="3"
                >
                  <VueMultiselect
                    v-model="selectedSVP"
                    :options="getApprover('SVP')"
                    label="ext_name"
                    :show-labels="false"
                  />
                </td>
              </tr>
              <tr>
                <td
                  style="
                    padding-bottom: 5px;
                    font-weight: bold;
                    text-align: center;
                    background-color: hsl(192, 51%, 90%);
                    font-size: 8pt;
                    border-top: 2px solid hsl(0, 0%, 0%);
                    border-right: 2px solid hsl(0, 0%, 0%);
                    border-left: 2px solid hsl(0, 0%, 0%);
                    height: 14.5pt;
                    width: 48pt;
                  "
                >
                  <p>件名</p>
                  <p>Subject</p>
                </td>
                <td
                  style="
                    padding: 5px;
                    word-wrap: break-word;
                    font-size: 10pt;
                    border-top: 2px solid hsl(0, 0%, 0%);
                    border-right: 2px solid hsl(0, 0%, 0%);
                    width: 284pt;
                    max-width: 284pt;
                  "
                  colspan="8"
                >
                  <input class="input__field" v-model="subject" />
                </td>
                <td
                  style="
                    padding-bottom: 5px;
                    font-weight: bold;
                    background-color: hsl(192, 51%, 90%);
                    font-size: 8pt;
                    border-top: 2px solid hsl(0, 0%, 0%);
                    border-right: 2px solid hsl(0, 0%, 0%);
                    width: 96pt;
                    max-width: 96pt;
                  "
                  colspan="2"
                >
                  <p>Conditions&nbsp;</p>
                  <p>(承認条件)</p>
                </td>
                <td
                  style="
                    border-right: 2px solid hsl(0, 0%, 0%);
                    border-top: 2px solid hsl(0, 0%, 0%);
                    width: 240pt;
                    padding-left: 3px;
                    padding-right: 3px;
                    font-size: 10pt;
                  "
                  colspan="5"
                >
                  <input class="input__field" v-model="condition" />
                </td>
              </tr>

              <tr>
                <td
                  style="
                    font-weight: bold;
                    text-align: center;
                    background-color: hsl(192, 51%, 90%);
                    vertical-align: top;
                    font-size: 8pt;
                    border: 2px solid hsl(0, 0%, 0%);
                    height: 14.5pt;
                    width: 48pt;
                  "
                  rowspan="6"
                >
                  <p>事後回覧</p>
                  <p>Related Partys</p>
                </td>
                <td
                  style="
                    font-weight: bold;
                    text-align: center;
                    font-size: 8pt;
                    border-top: 2px solid hsl(0, 0%, 0%);
                    border-right: 2px solid hsl(0, 0%, 0%);
                    width: 96pt;
                  "
                  colspan="1"
                  rowspan="2"
                >
                  <p>Party</p>
                  <p>(関係先)</p>
                </td>
                <td
                  style="
                    border-top: 2px solid hsl(0, 0%, 0%);
                    border-right: 2px solid hsl(0, 0%, 0%);
                    width: 96pt;
                    padding: 3px;
                    font-size: 10pt;
                  "
                  colspan="3"
                  rowspan="2"
                >
                  <VueMultiselect
                    v-model="selectedPartyOne"
                    :options="approvers"
                    label="ext_name"
                    :show-labels="false"
                  />
                </td>
                <td
                  style="
                    border-top: 2px solid hsl(0, 0%, 0%);
                    border-right: 2px solid hsl(0, 0%, 0%);
                    width: 96pt;
                    font-size: 10pt;
                    padding: 3px;
                  "
                  colspan="2"
                  rowspan="2"
                >
                  <VueMultiselect
                    v-model="selectedPartyTwo"
                    :options="approvers"
                    label="ext_name"
                    :show-labels="false"
                  />
                </td>
                <td
                  style="
                    border-top: 2px solid hsl(0, 0%, 0%);
                    border-right: 2px solid hsl(0, 0%, 0%);
                    width: 96pt;
                    font-size: 10pt;
                    padding: 3px;
                  "
                  colspan="2"
                  rowspan="2"
                >
                  <VueMultiselect
                    v-model="selectedPartyThree"
                    :options="approvers"
                    label="ext_name"
                    :show-labels="false"
                  />
                </td>
                <td
                  style="
                    border-top: 2px solid hsl(0, 0%, 0%);
                    border-right: 2px solid hsl(0, 0%, 0%);
                    width: 96pt;
                    font-size: 10pt;
                    padding: 3px;
                  "
                  colspan="2"
                  rowspan="2"
                >
                  <VueMultiselect
                    v-model="selectedPartyFour"
                    :options="approvers"
                    label="ext_name"
                    :show-labels="false"
                  />
                </td>
                <td
                  style="
                    font-weight: bold;
                    font-size: 8pt;
                    border-top: 2px solid hsl(0, 0%, 0%);
                    border-right: 2px solid hsl(0, 0%, 0%);
                    width: 96pt;
                  "
                  colspan="2"
                  rowspan="2"
                >
                  <p>Checked by&nbsp;</p>
                  <p>(検印)</p>
                </td>
                <td
                  style="
                    font-size: 10pt;
                    border-top: 2px solid hsl(0, 0%, 0%);
                    border-right: 2px solid hsl(0, 0%, 0%);
                    width: 144pt;
                    padding-left: 3px;
                  "
                  colspan="3"
                  rowspan="2"
                >
                  <div
                    style="
                      display: flex;
                      flex-direction: row;
                      gap: 4pt;
                      padding: 3pt;
                    "
                  >
                    <VueMultiselect
                      @select="selectChecker"
                      @remove="removeSelectedChecker"
                      v-model="selectedChecker"
                      :options="checkers"
                      label="ext_name"
                      :multiple="true"
                      :taggable="true"
                      :show-labels="false"
                      :loading="isLoading"
                      track-by="user_id"
                    />
                  </div>
                </td>
              </tr>

              <tr></tr>

              <tr>
                <td
                  style="
                    font-weight: bold;
                    text-align: center;
                    font-size: 8pt;
                    border-top: 2px solid hsl(0, 0%, 0%);
                    border-right: 2px solid hsl(0, 0%, 0%);
                    width: 96pt;
                  "
                  colspan="1"
                >
                  <p>Date&nbsp;</p>
                  <p>(日付)</p>
                </td>
                <td
                  style="
                    border-top: 2px solid hsl(0, 0%, 0%);
                    border-right: 2px solid hsl(0, 0%, 0%);
                    width: 96pt;
                  "
                  colspan="3"
                  rowspan="2"
                >
                  &nbsp;
                </td>
                <td
                  style="
                    border-top: 2px solid hsl(0, 0%, 0%);
                    border-right: 2px solid hsl(0, 0%, 0%);
                    width: 96pt;
                  "
                  colspan="2"
                  rowspan="2"
                >
                  &nbsp;
                </td>
                <td
                  style="
                    border-top: 2px solid hsl(0, 0%, 0%);
                    border-right: 2px solid hsl(0, 0%, 0%);
                    width: 96pt;
                  "
                  colspan="2"
                  rowspan="2"
                >
                  &nbsp;
                </td>
                <td
                  style="
                    border-top: 2px solid hsl(0, 0%, 0%);
                    border-right: 2px solid hsl(0, 0%, 0%);
                    width: 96pt;
                  "
                  colspan="2"
                  rowspan="2"
                >
                  &nbsp;
                </td>
                <td
                  style="
                    font-weight: bold;
                    font-size: 8pt;
                    border-top: 2px solid hsl(0, 0%, 0%);
                    border-right: 2px solid hsl(0, 0%, 0%);
                    width: 96pt;
                  "
                  colspan="2"
                >
                  <p>Prepared by&nbsp;</p>
                  <p>(作成者)</p>
                </td>
                <td
                  style="
                    padding-left: 3px;
                    font-size: 10pt;
                    border-top: 2px solid hsl(0, 0%, 0%);
                    border-right: 2px solid hsl(0, 0%, 0%);
                    width: 144pt;
                  "
                  colspan="3"
                  rowspan="2"
                >
                  <div></div>
                </td>
              </tr>
              <tr></tr>
              <tr>
                <td
                  style="
                    padding-bottom: 5px;
                    font-weight: bold;
                    text-align: center;
                    font-size: 8pt;
                    border-top: 2px dashed hsl(0, 0%, 0%);
                    border-right: 2px solid hsl(0, 0%, 0%);
                    border-bottom: 2px solid hsl(0, 0%, 0%);
                    width: 96pt;
                  "
                  colspan="1"
                >
                  <p>Seal</p>
                  <p>(印)</p>
                </td>
                <td
                  style="
                    border-top: 2px dashed hsl(0, 0%, 0%);
                    border-right: 2px solid hsl(0, 0%, 0%);
                    border-bottom: 2px solid hsl(0, 0%, 0%);
                    width: 96pt;
                  "
                  colspan="3"
                  rowspan="2"
                >
                  &nbsp;
                </td>
                <td
                  style="
                    border-top: 2px dashed hsl(0, 0%, 0%);
                    border-right: 2px solid hsl(0, 0%, 0%);
                    border-bottom: 2px solid hsl(0, 0%, 0%);
                    width: 96pt;
                  "
                  colspan="2"
                  rowspan="2"
                >
                  &nbsp;
                </td>
                <td
                  style="
                    border-top: 2px dashed hsl(0, 0%, 0%);
                    border-right: 2px solid hsl(0, 0%, 0%);
                    border-bottom: 2px solid hsl(0, 0%, 0%);
                    width: 96pt;
                  "
                  colspan="2"
                  rowspan="2"
                >
                  &nbsp;
                </td>
                <td
                  style="
                    border-top: 2px dashed hsl(0, 0%, 0%);
                    border-right: 2px solid hsl(0, 0%, 0%);
                    border-bottom: 2px solid hsl(0, 0%, 0%);
                    width: 96pt;
                  "
                  colspan="2"
                  rowspan="2"
                >
                  &nbsp;
                </td>
                <td
                  style="
                    font-weight: bold;
                    padding-bottom: 5px;
                    font-size: 8pt;
                    border-top: 2px solid hsl(0, 0%, 0%);
                    border-right: 2px solid hsl(0, 0%, 0%);
                    border-bottom: 2px solid hsl(0, 0%, 0%);
                    width: 96pt;
                  "
                  colspan="2"
                >
                  <p>Division&nbsp;</p>
                  <p>(作成部署)</p>
                </td>
                <td
                  style="
                    padding: 5px;
                    font-size: 10pt;
                    border-top: 2px solid hsl(0, 0%, 0%);
                    border-right: 2px solid hsl(0, 0%, 0%);
                    border-bottom: 2px solid hsl(0, 0%, 0%);
                    width: 144pt;
                  "
                  colspan="3"
                  rowspan="2"
                ></td>
              </tr>
              <tr></tr>
              <tr>
                <td style="height: 14.5pt; width: 768pt" colspan="16">
                  &nbsp;
                </td>
              </tr>
              <tr>
                <td
                  style="
                    font-size: 10pt;
                    padding-bottom: 5px;
                    background-color: hsl(192, 51%, 90%);
                    border-left: 2px solid hsl(0, 0%, 0%);
                    border-right: 2px solid hsl(0, 0%, 0%);
                    font-weight: bold;
                    height: 14.5pt;
                    width: 768pt;
                  "
                  colspan="16"
                >
                  1.&nbsp;&nbsp;&nbsp;&nbsp; BACKGROUND (背景)
                </td>
              </tr>
              <tr>
                <td
                  class="application_field"
                  style="
                    height: 40pt;
                    width: 768pt;
                    padding: 5px;
                    font-size: 10pt;
                    word-wrap: break-word;
                    max-width: 590pt;
                  "
                  colspan="16"
                >
                  <ckeditor
                    @input="changeBackground"
                    v-model="background"
                    :editor="editor"
                  ></ckeditor>
                </td>
              </tr>
              <tr>
                <td
                  style="
                    padding-bottom: 5px;
                    font-size: 10pt;
                    background-color: hsl(192, 51%, 90%);
                    border-left: 2px solid hsl(0, 0%, 0%);
                    border-right: 2px solid hsl(0, 0%, 0%);
                    font-weight: bold;
                    height: 14.5pt;
                    width: 768pt;
                  "
                  colspan="16"
                >
                  2.&nbsp;&nbsp;&nbsp;&nbsp; CONTENTS (稟議内容)
                </td>
              </tr>
              <tr>
                <td
                  class="application_field"
                  style="
                    height: 40pt;
                    width: 768pt;
                    padding: 5px;
                    font-size: 10pt;
                    word-wrap: break-word;
                    max-width: 590pt;
                  "
                  colspan="16"
                >
                  <ckeditor :editor="editor" v-model="content"></ckeditor>
                </td>
              </tr>
              <tr>
                <td
                  style="
                    padding-bottom: 5px;
                    font-size: 10pt;
                    background-color: hsl(192, 51%, 90%);
                    border-left: 2px solid hsl(0, 0%, 0%);
                    border-right: 2px solid hsl(0, 0%, 0%);
                    font-weight: bold;
                    height: 14.5pt;
                    width: 768pt;
                  "
                  colspan="16"
                >
                  3.&nbsp;&nbsp;&nbsp;&nbsp; REASONS (稟議理由)
                </td>
              </tr>
              <tr>
                <td
                  class="application_field"
                  style="
                    height: 40pt;
                    width: 768pt;
                    padding: 5px;
                    font-size: 10pt;
                    word-wrap: break-word;
                    max-width: 590pt;
                  "
                  colspan="16"
                >
                  <ckeditor :editor="editor" v-model="reason"></ckeditor>
                </td>
              </tr>
              <tr>
                <td
                  style="
                    padding-bottom: 5px;
                    font-size: 10pt;
                    background-color: hsl(192, 51%, 90%);
                    border-left: 2px solid hsl(0, 0%, 0%);
                    border-right: 2px solid hsl(0, 0%, 0%);
                    font-weight: bold;
                    height: 14.5pt;
                    width: 768pt;
                  "
                  colspan="16"
                >
                  4.&nbsp;&nbsp;&nbsp;&nbsp; OTHERS (その他)
                </td>
              </tr>
              <tr>
                <td
                  class="application_field"
                  style="
                    height: 40pt;
                    width: 768pt;
                    padding: 5px;
                    font-size: 10pt;
                    word-wrap: break-word;
                    max-width: 590pt;
                  "
                  colspan="16"
                >
                  <ckeditor :editor="editor" v-model="other"></ckeditor>
                </td>
              </tr>
              <tr>
                <td
                  style="
                    padding-bottom: 5px;
                    font-size: 10pt;
                    background-color: hsl(192, 51%, 90%);
                    border-left: 2px solid hsl(0, 0%, 0%);
                    border-right: 2px solid hsl(0, 0%, 0%);
                    font-weight: bold;
                    height: 14.5pt;
                    width: 768pt;
                  "
                  colspan="16"
                >
                  5.&nbsp;&nbsp;&nbsp;&nbsp; ATTACHED DOCUMENTS (添付資料)
                </td>
              </tr>
              <tr>
                <td
                  style="height: 40pt; width: 768pt; padding: 5pt"
                  colspan="16"
                >
                  <FileUploader :isMultiple="true" />
                </td>
              </tr>
            </tbody>
          </table>
        </figure>
      </div>
    </template>
    <template v-slot:footer>
      <div class="w-full">
        <div class="w-44 flex mx-auto">
          <button
            class="button-primary mr-2"
            :disabled="isProcessing"
            @click="submitTicket"
          >
            {{ isProcessing ? "Submiting..." : "Submit" }}
          </button>
          <button
            :disabled="isProcessing"
            class="button-transparent border"
            @click="$emit('close')"
          >
            Cancel
          </button>
        </div>
      </div>
    </template>
  </Modal>
</template>

<script>
import CKEditor from "@ckeditor/ckeditor5-vue";
import ClassicEditor from "@/util/ClassicEditor.js";
import FileUploader from "@/components/FileUploader.vue";
import Modal from "@/components/Modal.vue";
import VueMultiselect from "vue-multiselect";

import { useStore } from "vuex";
import { computed, onMounted, onUnmounted, ref } from "vue";
import { SIGNATORY_TYPE } from "@/util/constant";
import { formatDateTime } from "@/util/helper";
import { Signatory } from "@/models/Signatory";
import { useSignalR } from "@quangdao/vue-signalr";

export default {
  emits: ["close"],
  components: {
    Modal,
    VueMultiselect,
    FileUploader,
    ckeditor: CKEditor.component,
  },

  setup() {
    const signalR = useSignalR();
    const PENDING_STATUS = 1;
    const currentDate = new Date();

    const isProcessing = computed(() => store.state.app.isProcessing);
    const connectionId = signalR.connection.connectionId;
    const formattedDatetime = formatDateTime(currentDate);

    const store = useStore();

    const selectedPresident = ref({});
    const selectedCFO = ref({});
    const selectedEVP = ref({});
    const selectedSVP = ref({});

    const selectedPartyOne = ref({});
    const selectedPartyTwo = ref({});
    const selectedPartyThree = ref({});
    const selectedPartyFour = ref({});

    const background = ref("");
    const subject = ref("");
    const condition = ref("");
    const content = ref("");
    const reason = ref("");
    const other = ref("");

    const editor = ClassicEditor;
    const approvers = ref([]);
    const checkers = ref([]);
    const relatedParty = ref([]);
    const priorities = ref([]);

    const isLoading = ref(false);
    const selectedChecker = ref([]);
    const selectedApprover = ref([]);
    const selectedRelatedPary = ref([]);
    const selectedPriority = ref(priorities.value[0]);

    const uploadedFiles = computed(() => store.getters["file/files"]);

    const currentUser = JSON.parse(localStorage.getItem("user"));

    const selectedSignatories = () => {
      var formattedApprover = [...selectedApprover.value].map(
        (approver) =>
          new Signatory({
            user_id: approver.user_id,
            type: SIGNATORY_TYPE.APPROVER,
          })
      );

      var formattedChecker = [...selectedChecker.value].map(
        (checker) =>
          new Signatory({
            user_id: checker.user_id,
            type: SIGNATORY_TYPE.CHECKER,
          })
      );

      var formattedParty = [...selectedRelatedPary.value].map(
        (party) =>
          new Signatory({
            user_id: party.user_id,
            type: SIGNATORY_TYPE.PARTY,
          })
      );

      var signatories = formattedApprover.concat(
        formattedChecker,
        formattedParty
      );

      return signatories;
    };

    const validate = () => {
      var alert;
      var hasError = false;
      if (!subject.value) {
        alert = {
          type: "danger",
          message: "Please fill up the subject field.",
        };
        hasError = true;
      } else if (!background.value) {
        alert = {
          type: "danger",
          message: "Please fill up the background field.",
        };
        hasError = true;
      } else if (!content.value) {
        alert = {
          type: "danger",
          message: "Please fill up the content field.",
        };
        hasError = true;
      } else if (!reason.value) {
        alert = { type: "danger", message: "Please fill up the reason field." };
        hasError = true;
      } else if (!selectedApprover.value.length) {
        alert = {
          type: "danger",
          message: "Please select at least 1 approver.",
        };
        hasError = true;
      } else if (!selectedChecker.value.length) {
        alert = {
          type: "danger",
          message: "Please select at least 1 checker.",
        };
        hasError = true;
      }

      if (hasError) {
        store.dispatch("app/addAlert", alert);
      }

      return hasError;
    };

    function selectChecker(selectedOption) {
      const newRelatedParty = [...relatedParty.value].filter(
        (u) => u.user_id != selectedOption.user_id
      );
      relatedParty.value = newRelatedParty;
    }

    function removeSelectedChecker(removedOption) {
      relatedParty.value = relatedParty.value.concat(removedOption);
    }

    function selectApprover(selectedOption) {
      approvers.value = [...approvers.value].filter(
        (value) => value.user_id !== selectedOption.user_id
      );
    }

    //get approver based on position
    function getApprover(position) {
      return [...approvers.value].filter(
        (value) => value.job_title.name === position
      );
    }

    function removeSelectedApprover(removedOption) {
      relatedParty.value = relatedParty.value.concat(removedOption);
    }

    function selectRelatedParty(selectedOption) {
      const newCheckers = [...checkers.value].filter(
        (u) => u.user_id != selectedOption.user_id
      );
      const newApprovers = [...approvers.value].filter(
        (u) => u.user_id != selectedOption.user_id
      );

      checkers.value = newCheckers;
      approvers.value = newApprovers;
    }

    function removeSelectedParty(removedOption) {
      switch (removedOption.role.name) {
        case "Checker":
          checkers.value = checkers.value.concat(removedOption);
          break;
        case "Approver":
          approvers.value = approvers.value.concat(removedOption);
          break;
      }
    }

    const changeBackground = (value) => {
      console.log(value);
    };

    const submitTicket = async () => {
      if (!validate()) {
        var formData = new FormData();
        uploadedFiles.value.forEach((file) => {
          formData.append("files", file);
        });
        formData.append("others", other.value);
        formData.append("connection_id", connectionId);
        formData.append("subject", subject.value);
        formData.append("condition", condition.value);
        formData.append("background", background.value);
        formData.append("content", content.value);
        formData.append("reason", reason.value);
        formData.append("status_id", PENDING_STATUS);
        formData.append("created_by", currentUser.user_id);
        formData.append("priority_id", selectedPriority.value.priority_id);
        formData.append("date_created", formattedDatetime);
        formData.append("signatories", JSON.stringify(selectedSignatories()));

        await store.dispatch("ticket/createTicket", formData);
      }
    };

    onMounted(async () => {
      const param = {
        department_id: currentUser.department.department_id,
        user_id: currentUser.user_id,
      };

      store.commit("app/SET_MODAL_LOADING", true);
      await store.dispatch("user/fetchApprovers");
      await store.dispatch("user/fetchCheckers", param);
      await store.dispatch("user/fetchRelatedParties", currentUser.user_id);
      await store.dispatch("priority/fetchPriorities");

      store.commit("app/SET_MODAL_LOADING", false);
      isLoading.value = store.state.app.isLoading;
      approvers.value = [...store.state.user.approvers].map((u) => u.user);
      checkers.value = [...store.state.user.checkers].map((u) => u.user);
      relatedParty.value = [...store.state.user.relatedParties].map(
        (u) => u.user
      );
      priorities.value = store.state.priority.priorities;
      selectedPriority.value = store.state.priority.priorities[0];
    });

    onUnmounted(() => {
      store.commit("file/EMPTY_FILES", []);
    });

    return {
      submitTicket,
      selectChecker,
      removeSelectedChecker,
      selectRelatedParty,
      removeSelectedParty,
      selectApprover,
      changeBackground,
      getApprover,
      editor,
      selectedChecker,
      selectedApprover,
      selectedRelatedPary,
      background,
      subject,
      condition,
      content,
      reason,
      other,
      approvers,
      checkers,
      relatedParty,
      isLoading,
      priorities,
      selectedPriority,
      isProcessing,

      selectedPresident,
      selectedCFO,
      selectedEVP,
      selectedSVP,

      selectedPartyOne,
      selectedPartyTwo,
      selectedPartyThree,
      selectedPartyFour,
    };
  },
};
</script>

<style scoped>
:deep(.multiselect span),
:deep(.multiselect input) {
  font-size: 10pt !important;
}
</style>
