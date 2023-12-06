<template>
  <Modal @close="$emit('close')">
    <template v-slot:header>
      <h5 class="modal-title">{{ ticketData?.ticket_id }}</h5>
    </template>
    <template v-slot:content>
      <div class="ticket_form_container">
        <div
          v-if="ticketData?.status?.name === TICKET_STATUS.REJECTED"
          class="bg-[#ffcdcd] border-[3px] rounded-sm text-sm border-red border-solid p-2 w-full flex flex-col justify-start"
        >
          <p class="font-bold mb-2">NOTE</p>
          <span class="font-semibold">{{ makeRejectionNote() }}</span>
        </div>
        <Vue3Html2pdf
          :paginate-elements-by-height="1400"
          :show-layout="true"
          :float-layout="false"
          :enable-download="true"
          :pdf-quality="2"
          :filename="fileName"
          ref="pdf"
          pdf-format="letter"
          pdf-content-width="800px"
        >
          <template v-slot:pdf-content>
            <figure class="table" style="width: 790px; padding: 10px">
              <table
                class="ck-table-resized"
                style="border: 3px double hsl(0, 0%, 0%)"
              >
                <colgroup>
                  <col style="width: 10.07%" />
                  <col style="width: 5.61%" />
                  <col style="width: 5.27%" />
                  <col style="width: 5.27%" />
                  <col style="width: 5.27%" />
                  <col style="width: 4.57%" />
                  <col style="width: 5.27%" />
                  <col style="width: 5.27%" />
                  <col style="width: 5.27%" />
                  <col style="width: 5.27%" />
                  <col style="width: 5.27%" />
                  <col style="width: 5.27%" />
                  <col style="width: 5.27%" />
                  <col style="width: 12.5%" />
                  <col style="width: 9.27%" />
                  <col style="width: 5.28%" />
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
                    >
                      {{ getDate(TICKET_STATUS.APPROVED) }}
                    </td>
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
                    >
                      {{
                        ticketData?.date_created
                          ? formatDate(ticketData.date_created)
                          : ""
                      }}
                    </td>
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
                      style="
                        border-bottom: 2px solid hsl(0, 0%, 0%);
                        width: 96pt;
                      "
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
                      colspan="5"
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
                      colspan="6"
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
                      colspan="4"
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
                      colspan="2"
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
                      colspan="2"
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
                      colspan="2"
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
                      colspan="2"
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
                      colspan="1"
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
                        width: 96pt;
                      "
                      colspan="1"
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
                        width: 48pt;
                      "
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
                    >
                      SVP
                    </td>
                  </tr>

                  <tr>
                    <td
                      style="
                        border-bottom: 2px dashed hsl(0, 0%, 0%);
                        border-right: 2px solid hsl(0, 0%, 0%);
                        width: 96pt;
                      "
                      colspan="2"
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
                      colspan="2"
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
                      <img
                        style="width: 40pt"
                        draggable="false"
                        :src="getApproverSignature(JOB_TITLE.PRESIDENT)"
                      />
                    </td>
                    <td
                      style="
                        border-bottom: 2px dashed hsl(0, 0%, 0%);
                        border-right: 2px solid hsl(0, 0%, 0%);
                        width: 96pt;
                      "
                      colspan="1"
                    >
                      <img
                        style="width: 40pt"
                        draggable="false"
                        :src="getApproverSignature(JOB_TITLE.CFO)"
                      />
                    </td>
                    <td
                      style="
                        border-bottom: 2px dashed hsl(0, 0%, 0%);
                        border-right: 2px solid hsl(0, 0%, 0%);
                        width: 48pt;
                      "
                    >
                      <img
                        style="width: 40pt"
                        draggable="false"
                        :src="getApproverSignature(JOB_TITLE.EVP)"
                      />
                    </td>
                    <td
                      style="
                        border-bottom: 2px dashed hsl(0, 0%, 0%);
                        width: 48pt;
                      "
                    >
                      <img
                        style="width: 40pt"
                        draggable="false"
                        :src="getApproverSignature(JOB_TITLE.SVP)"
                      />
                    </td>
                  </tr>

                  <tr>
                    <td
                      style="
                        border-right: 2px solid hsl(0, 0%, 0%);
                        width: 96pt;
                      "
                      colspan="2"
                    >
                      &nbsp;
                    </td>
                    <td
                      style="
                        border-right: 2px solid hsl(0, 0%, 0%);
                        width: 48pt;
                      "
                    >
                      &nbsp;
                    </td>
                    <td
                      style="
                        border-right: 2px solid hsl(0, 0%, 0%);
                        width: 96pt;
                      "
                      colspan="2"
                    >
                      &nbsp;
                    </td>
                    <td
                      style="
                        border-right: 2px solid hsl(0, 0%, 0%);
                        width: 96pt;
                      "
                      colspan="2"
                    >
                      &nbsp;
                    </td>
                    <td
                      style="
                        border-right: 2px solid hsl(0, 0%, 0%);
                        width: 96pt;
                      "
                      colspan="2"
                    >
                      &nbsp;
                    </td>
                    <td
                      style="
                        border-right: 2px solid hsl(0, 0%, 0%);
                        width: 48pt;
                      "
                    >
                      &nbsp;
                    </td>
                    <td
                      style="
                        border-right: 2px solid hsl(0, 0%, 0%);
                        width: 48pt;
                      "
                    >
                      &nbsp;
                    </td>
                    <td
                      style="
                        border-right: 2px solid hsl(0, 0%, 0%);
                        width: 96pt;
                      "
                      colspan="1"
                    >
                      {{
                        getApprover(JOB_TITLE.PRESIDENT)?.user?.user?.short_name
                      }}
                    </td>
                    <td
                      style="
                        border-right: 2px solid hsl(0, 0%, 0%);
                        width: 96pt;
                        font-size: 10pt;
                      "
                      colspan="1"
                    >
                      {{ getApprover(JOB_TITLE.CFO)?.user?.user?.short_name }}
                    </td>
                    <td
                      style="
                        border-right: 2px solid hsl(0, 0%, 0%);
                        width: 48pt;
                        font-size: 10pt;
                      "
                    >
                      {{ getApprover(JOB_TITLE.EVP)?.user?.user?.short_name }}
                    </td>
                    <td style="width: 48pt; font-size: 10pt">
                      {{ getApprover(JOB_TITLE.SVP)?.user?.user?.short_name }}
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
                      {{ ticketData?.subject }}
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
                        font-size: 10pt;
                      "
                      colspan="5"
                    >
                      {{ ticketData?.condition }}
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
                      colspan="2"
                      rowspan=""
                    >
                      <p>Party</p>
                      <p>(関係先)</p>
                    </td>
                    <td
                      style="
                        border-top: 2px solid hsl(0, 0%, 0%);
                        border-right: 2px solid hsl(0, 0%, 0%);
                        width: 96pt;
                        font-size: 10pt;
                      "
                      colspan="2"
                      rowspan="2"
                    >
                      {{ getRelatedParties(0)?.user?.user?.ext_name }}
                    </td>
                    <td
                      style="
                        border-top: 2px solid hsl(0, 0%, 0%);
                        border-right: 2px solid hsl(0, 0%, 0%);
                        width: 96pt;
                        font-size: 10pt;
                      "
                      colspan="2"
                      rowspan="2"
                    >
                      {{ getRelatedParties(1)?.user?.user?.ext_name }}
                    </td>
                    <td
                      style="
                        border-top: 2px solid hsl(0, 0%, 0%);
                        border-right: 2px solid hsl(0, 0%, 0%);
                        width: 96pt;
                        font-size: 10pt;
                      "
                      colspan="2"
                      rowspan="2"
                    >
                      {{ getRelatedParties(2)?.user?.user?.ext_name }}
                    </td>
                    <td
                      style="
                        border-top: 2px solid hsl(0, 0%, 0%);
                        border-right: 2px solid hsl(0, 0%, 0%);
                        width: 96pt;
                        font-size: 10pt;
                      "
                      colspan="2"
                      rowspan="2"
                    >
                      {{ getRelatedParties(3)?.user?.user?.ext_name }}
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
                      <div style="display: flex; flex-direction: row; gap: 4pt">
                        <div v-for="checker in getCheckers()">
                          <img
                            style="width: 60pt"
                            draggable="false"
                            :src="getCheckerSignature(checker)"
                          />
                          <p>{{ checker?.user?.user?.ext_name }}</p>
                        </div>
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
                      colspan="2"
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
                      <div>
                        <img
                          style="width: 60pt"
                          draggable="false"
                          :src="
                            getFileLink(
                              ticketData?.created_by?.file?.stored_file_name
                            )
                          "
                        />
                        <p>
                          {{ ticketData?.created_by?.user?.ext_name }}
                        </p>
                      </div>
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
                      colspan="2"
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
                    >
                      {{ ticketData?.created_by?.user?.department?.name }}
                    </td>
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
                      v-html="ticketData?.background"
                      style="
                        height: 40pt;
                        width: 768pt;
                        padding: 5px;
                        font-size: 10pt;
                        word-wrap: break-word;
                        max-width: 590pt;
                      "
                      colspan="16"
                    ></td>
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
                      v-html="ticketData?.content"
                      style="
                        height: 40pt;
                        width: 768pt;
                        padding: 5px;
                        font-size: 10pt;
                        word-wrap: break-word;
                        max-width: 590pt;
                      "
                      colspan="16"
                    ></td>
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
                      v-html="ticketData?.reason"
                      style="
                        height: 40pt;
                        width: 768pt;
                        padding: 5px;
                        font-size: 10pt;
                        word-wrap: break-word;
                        max-width: 590pt;
                      "
                      colspan="16"
                    ></td>
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
                      v-html="ticketData?.other"
                      style="
                        height: 40pt;
                        width: 768pt;
                        padding: 5px;
                        font-size: 10pt;
                        word-wrap: break-word;
                        max-width: 590pt;
                      "
                      colspan="16"
                    ></td>
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
                      <div>
                        <div v-for="file in files">
                          <a
                            style="font-size: 10pt"
                            :href="getFileLink(file.stored_file_name)"
                            >{{ file.original_file_name }}</a
                          >
                        </div>
                      </div>
                    </td>
                  </tr>
                </tbody>
              </table>
            </figure>
          </template>
        </Vue3Html2pdf>
        <br />
      </div>
    </template>
    <template v-slot:footer>
      <div class="w-full flex justify-center">
        <div
          class="flex"
          v-if="
            currentUser?.role?.name === ROLE.ADMIN &&
            ticketData?.status?.name === TICKET_STATUS.APPROVED
          "
        >
          <button class="button-primary mr-2" @click="done">Done</button>
          <button class="button-transparent border mr-2">Cancel</button>
        </div>

        <div
          v-if="currentSignatoryData?.status?.name === TICKET_STATUS.PENDING"
          class="w-full flex justify-center"
        >
          <button
            class="button-primary mr-2"
            :disabled="isProcessing"
            @click="approved"
          >
            {{ isProcessing ? "Approving..." : "Approve" }}
          </button>

          <button class="button-transparent border" @click="openRejectionModal">
            Reject
          </button>
        </div>

        <button
          v-if="ticketData?.status?.name !== TICKET_STATUS.PENDING"
          class="button-primary w-fit"
          @click="generatePDF()"
        >
          Download As PDF
        </button>
      </div>
    </template>
  </Modal>
</template>
<script>
import Modal from "@/components/Modal.vue";
import Vue3Html2pdf from "vue3-html2pdf";
import jsPDF from "jspdf";
import { useStore } from "vuex";
import { useRoute } from "vue-router";
import { ref, watch, computed } from "vue";
import { useSignalR } from "@quangdao/vue-signalr";
import {
  TICKET_STATUS,
  ROLE,
  JOB_TITLE,
  SIGNATORY_TYPE,
} from "@/util/constant";
import { formatDate } from "@/util/helper";
const BASE_URL = import.meta.env.VITE_BASE_URL;

export default {
  emits: ["close"],
  components: {
    Modal,
    Vue3Html2pdf,
    jsPDF,
  },

  setup() {
    const signalR = useSignalR();
    const pdf = ref(null);
    const store = useStore();
    const route = useRoute();
    const ticketData = ref({});
    const currentSignatoryData = ref({});
    const signatories = ref([]);
    const currentUser = JSON.parse(localStorage.getItem("user"));
    const isProcessing = computed(() => store.state.app.isProcessing);
    const files = ref([]);
    const fileName = ref(null);

    const approved = async () => {
      const signatoryId = currentSignatoryData.value.signatory_id;
      const connectionId = signalR.connection.connectionId;

      const approver = {
        signatory_id: signatoryId,
        connection_id: connectionId,
      };

      await store.dispatch("ticket/approveTicket", approver);
    };

    const generatePDF = () => {
      pdf.value.generatePdf();
    };

    const done = async () => {
      const currentUserId = currentUser.user_id;
      const connectionId = signalR.connection.connectionId;
      const ticketId = ticketData.value.ticket_id;

      const request = {
        connection_id: connectionId,
        ticket_id: ticketId,
        user_id: currentUserId,
      };

      await store.dispatch("ticket/doneTicket", request);
    };

    const openRejectionModal = () => {
      const signatoryId = currentSignatoryData.value.signatory_id;
      const connectionId = signalR.connection.connectionId;
      store.commit("app/SET_SIGNATORY", {
        signatoryId: signatoryId,
        connectionId: connectionId,
      });
      store.commit("app/SET_REJECTION_REASON_MODAL", true);
    };

    const getApprover = (jobTitle) => {
      if (ticketData.value?.subject) {
        return signatories.value.find(
          (s) => s.user.user.job_title.name === jobTitle
        );
      }
      return null;
    };

    const getFileLink = (fileName) => {
      return fileName ? `${BASE_URL}/File/${fileName}` : "";
    };

    const getCheckers = () => {
      return signatories.value.filter((c) => c.type === SIGNATORY_TYPE.CHECKER);
    };

    const getDate = (ticketStatus) => {
      return ticketData.value?.status?.name === ticketStatus ||
        ticketData.value?.status?.name === TICKET_STATUS.DONE
        ? formatDate(ticketData.value.action_date)
        : "";
    };

    const makeRejectionNote = () => {
      if (ticketData.value?.subject) {
        const rejectedBy = ticketData.value?.rejected_by?.user?.ext_name;
        const dateRejected = new Intl.DateTimeFormat("en-GB", {
          dateStyle: "medium",
        }).format(new Date(ticketData.value?.action_date));

        const reason = ticketData.value?.rejection_reason;
        return `Rejected (by ${rejectedBy} on ${dateRejected}); Reason: ${reason}`;
      }
      return null;
    };

    const getRelatedParties = (num) => {
      const relatedParties = signatories.value.filter(
        (c) => c.type === SIGNATORY_TYPE.PARTY
      );
      if (relatedParties?.length !== 0) {
        if (relatedParties?.length - 1 < num) {
          return null;
        }
        return relatedParties[num];
      }
      return null;
    };

    const getApproverSignature = (jobTitle) => {
      return getFileLink(
        getApprover(jobTitle)?.status?.name === TICKET_STATUS.APPROVED
          ? getApprover(jobTitle)?.user?.file?.stored_file_name
          : ""
      );
    };

    const getCheckerSignature = (checker) => {
      return checker?.status?.name === TICKET_STATUS.APPROVED
        ? getFileLink(checker?.user?.file?.stored_file_name)
        : "";
    };

    watch(
      () => route.params.ticketId,
      async (newTicketId, oldTicketId) => {
        if (newTicketId) {
          await store.dispatch("ticket/fetchTicket", newTicketId);
          ticketData.value = store.state.ticket.ticket.ticket;
          currentSignatoryData.value =
            store.state.ticket.ticket.signatories.find(
              (s) => s.user.user.user_id === currentUser.user_id
            );
          files.value = store.state.ticket.ticket.files;
          signatories.value = store.state.ticket.ticket.signatories;
          fileName.value = `Application No.${ticketData.value.ticket_id}`;
          console.log(ticketData.value);
        }
      },
      { immediate: true }
    );

    return {
      ticketData,
      currentSignatoryData,
      signatories,
      isProcessing,
      TICKET_STATUS,
      JOB_TITLE,
      ROLE,
      formatDate,
      currentUser,
      pdf,
      files,
      fileName,
      getDate,
      getCheckers,
      approved,
      done,
      openRejectionModal,
      getApprover,
      getFileLink,
      getApproverSignature,
      getCheckerSignature,
      generatePDF,
      getRelatedParties,
      makeRejectionNote,
    };
  },
};
</script>
