<template>
  <Modal @close="$emit('close')">
    <template v-slot:header>
      <h5 class="modal-title">{{ ticketData?.ticket_id }}</h5>
    </template>
    <template v-slot:content>
      <div class="ticket_form_container">
        <figure class="table" style="width: 816px">
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
                    border: 2px solid hsl(0, 0%, 0%);
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
                    border: 2px solid hsl(0, 0%, 0%);
                    width: 96pt;
                    font-size: 8pt;
                    font-weight: bold;
                  "
                  colspan="2"
                >
                  Applied Date
                </td>
                <td
                  style="
                    padding-left: 3px;
                    font-size: 9pt;
                    border: 2px solid hsl(0, 0%, 0%);
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
                    border: 2px solid hsl(0, 0%, 0%);
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
                    border: 2px solid hsl(0, 0%, 0%);
                    width: 192pt;
                    padding: 3px;
                  "
                  colspan="4"
                  rowspan="2"
                >
                  <!--Approved No-->
                </td>

                <td
                  style="width: 96pt; font-size: 8pt; font-weight: bold"
                  colspan="2"
                >
                  Receipt No.
                </td>

                <td
                  style="border: 2px solid hsl(0, 0%, 0%); width: 96pt"
                  colspan="2"
                >
                  <!--Receipt No-->
                </td>
              </tr>
              <tr>
                <td
                  style="font-weight: bold; width: 96pt; font-size: 8pt"
                  colspan="2"
                >
                  Receipt Date
                </td>
                <td
                  style="border: 2px solid hsl(0, 0%, 0%); width: 96pt"
                  colspan="2"
                >
                  <!--Receipt Date-->
                </td>
              </tr>

              <tr>
                <td
                  style="
                    text-align: center;
                    background-color: hsl(192, 51%, 90%);
                    font-size: 8pt;
                    border: 2px solid hsl(0, 0%, 0%);
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
                    font-size: 8pt;
                    border: 2px solid hsl(0, 0%, 0%);
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
                    font-size: 8pt;
                    border: 2px solid hsl(0, 0%, 0%);
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
                    font-size: 8pt;
                    background-color: hsl(192, 51%, 90%);
                    border: 2px solid hsl(0, 0%, 0%);
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
                    text-align: center;
                    font-weight: bold;
                    font-size: 8pt;
                    border: 2px solid hsl(0, 0%, 0%);
                    width: 96pt;
                  "
                  colspan="2"
                >
                  □CEO
                </td>
                <td
                  style="
                    text-align: center;
                    font-weight: bold;
                    font-size: 8pt;
                    border: 2px solid hsl(0, 0%, 0%);
                    width: 48pt;
                  "
                >
                  □COO
                </td>
                <td
                  style="
                    text-align: center;
                    font-weight: bold;
                    font-size: 8pt;
                    border: 2px solid hsl(0, 0%, 0%);
                    width: 96pt;
                  "
                  colspan="2"
                >
                  □担当役員
                </td>
                <td
                  style="
                    text-align: center;
                    font-weight: bold;
                    font-size: 8pt;
                    border: 2px solid hsl(0, 0%, 0%);
                    width: 96pt;
                  "
                  colspan="2"
                >
                  □部長
                </td>
                <td
                  style="
                    text-align: center;
                    font-weight: bold;
                    font-size: 8pt;
                    border: 2px solid hsl(0, 0%, 0%);
                    width: 96pt;
                  "
                  colspan="2"
                >
                  課長
                </td>
                <td
                  style="
                    text-align: center;
                    font-weight: bold;
                    font-size: 8pt;
                    border: 2px solid hsl(0, 0%, 0%);
                    width: 48pt;
                  "
                >
                  検印
                </td>
                <td
                  style="
                    text-align: center;
                    font-weight: bold;
                    font-size: 8pt;
                    border: 2px solid hsl(0, 0%, 0%);
                    width: 48pt;
                  "
                >
                  担当
                </td>
                <td
                  style="
                    text-align: center;
                    font-weight: bold;
                    font-size: 8pt;
                    border: 2px solid hsl(0, 0%, 0%);
                    width: 96pt;
                  "
                  colspan="1"
                >
                  □President
                </td>
                <td
                  style="
                    text-align: center;
                    font-weight: bold;
                    font-size: 8pt;
                    border: 2px solid hsl(0, 0%, 0%);
                    width: 96pt;
                  "
                  colspan="1"
                >
                  CFO
                </td>
                <td
                  style="
                    text-align: center;
                    font-weight: bold;
                    font-size: 8pt;
                    border: 2px solid hsl(0, 0%, 0%);
                    width: 48pt;
                  "
                >
                  EVP
                </td>
                <td
                  style="
                    text-align: center;
                    font-weight: bold;
                    font-size: 8pt;
                    border: 2px solid hsl(0, 0%, 0%);
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
                <td
                  style="border-bottom: 2px dashed hsl(0, 0%, 0%); width: 48pt"
                ></td>
              </tr>

              <tr>
                <td
                  style="border-right: 2px solid hsl(0, 0%, 0%); width: 96pt"
                  colspan="2"
                >
                  &nbsp;
                </td>
                <td style="border-right: 2px solid hsl(0, 0%, 0%); width: 48pt">
                  &nbsp;
                </td>
                <td
                  style="border-right: 2px solid hsl(0, 0%, 0%); width: 96pt"
                  colspan="2"
                >
                  &nbsp;
                </td>
                <td
                  style="border-right: 2px solid hsl(0, 0%, 0%); width: 96pt"
                  colspan="2"
                >
                  &nbsp;
                </td>
                <td
                  style="border-right: 2px solid hsl(0, 0%, 0%); width: 96pt"
                  colspan="2"
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
                  style="border-right: 2px solid hsl(0, 0%, 0%); width: 96pt"
                  colspan="1"
                >
                  {{ getApprover(JOB_TITLE.PRESIDENT) }}
                </td>
                <td
                  style="
                    border-right: 2px solid hsl(0, 0%, 0%);
                    width: 96pt;
                    font-size: 10pt;
                  "
                  colspan="1"
                >
                  {{ getApprover(JOB_TITLE.CFO) }}
                </td>
                <td
                  style="
                    border-right: 2px solid hsl(0, 0%, 0%);
                    width: 48pt;
                    font-size: 10pt;
                  "
                >
                  {{ getApprover(JOB_TITLE.EVP) }}
                </td>
                <td style="width: 48pt; font-size: 10pt">
                  {{ getApprover(JOB_TITLE.SVP) }}
                </td>
              </tr>
              <tr>
                <td
                  style="
                    font-weight: bold;
                    text-align: center;
                    background-color: hsl(192, 51%, 90%);
                    font-size: 8pt;
                    border: 2px solid hsl(0, 0%, 0%);
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
                    border: 2px solid hsl(0, 0%, 0%);
                    width: 284pt;
                    max-width: 284pt;
                  "
                  colspan="8"
                >
                  {{ ticketData?.subject }}
                </td>
                <td
                  style="
                    font-weight: bold;
                    background-color: hsl(192, 51%, 90%);
                    font-size: 8pt;
                    border: 2px solid hsl(0, 0%, 0%);
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
                    border: 2px solid hsl(0, 0%, 0%);
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
                    border: 2px solid hsl(0, 0%, 0%);
                    width: 96pt;
                  "
                  colspan="2"
                  rowspan=""
                >
                  <p>Party</p>
                  <p>(関係先)</p>
                </td>
                <td
                  style="border: 2px solid hsl(0, 0%, 0%); width: 96pt"
                  colspan="2"
                  rowspan="2"
                >
                  &nbsp;
                </td>
                <td
                  style="border: 2px solid hsl(0, 0%, 0%); width: 96pt"
                  colspan="2"
                  rowspan="2"
                >
                  &nbsp;
                </td>
                <td
                  style="border: 2px solid hsl(0, 0%, 0%); width: 96pt"
                  colspan="2"
                  rowspan="2"
                >
                  &nbsp;
                </td>
                <td
                  style="border: 2px solid hsl(0, 0%, 0%); width: 96pt"
                  colspan="2"
                  rowspan="2"
                >
                  &nbsp;
                </td>
                <td
                  style="
                    font-weight: bold;
                    font-size: 8pt;
                    border: 2px solid hsl(0, 0%, 0%);
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
                    border: 2px solid hsl(0, 0%, 0%);
                    width: 144pt;
                    padding-left: 3px;
                  "
                  colspan="3"
                  rowspan="2"
                >
                  <p v-for="checker in getCheckers()">
                    {{ checker?.user?.user?.ext_name }}
                  </p>
                </td>
              </tr>

              <tr></tr>

              <tr>
                <td
                  style="
                    font-weight: bold;
                    text-align: center;
                    font-size: 8pt;
                    border-bottom: 2px dashed hsl(0, 0%, 0%);
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
                    border-bottom: 2px dashed hsl(0, 0%, 0%);
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
                    border-bottom: 2px dashed hsl(0, 0%, 0%);
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
                    border-bottom: 2px dashed hsl(0, 0%, 0%);
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
                    border-bottom: 2px dashed hsl(0, 0%, 0%);
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
                    border: 2px solid hsl(0, 0%, 0%);
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
                    border: 2px solid hsl(0, 0%, 0%);
                    width: 144pt;
                  "
                  colspan="3"
                  rowspan="2"
                >
                  {{ ticketData?.created_by?.user?.ext_name }}
                </td>
              </tr>
              <tr></tr>
              <tr>
                <td
                  style="
                    font-weight: bold;
                    text-align: center;
                    font-size: 8pt;
                    border-bottom: 2px solid hsl(0, 0%, 0%);
                    border-right: 2px solid hsl(0, 0%, 0%);
                    width: 96pt;
                  "
                  colspan="2"
                >
                  <p>Seal</p>
                  <p>(印)</p>
                </td>
                <td
                  style="
                    border-bottom: 2px solid hsl(0, 0%, 0%);
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
                    border-bottom: 2px solid hsl(0, 0%, 0%);
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
                    border-bottom: 2px solid hsl(0, 0%, 0%);
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
                    border-bottom: 2px solid hsl(0, 0%, 0%);
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
                    border: 2px solid hsl(0, 0%, 0%);
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
                    border: 2px solid hsl(0, 0%, 0%);
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
                    background-color: hsl(192, 51%, 90%);
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
                  v-html="ticketData?.background"
                  style="
                    height: 116pt;
                    width: 768pt;
                    padding: 5px;
                    font-size: 10pt;
                  "
                  colspan="16"
                ></td>
              </tr>
              <tr>
                <td
                  style="
                    font-size: 10pt;
                    background-color: hsl(192, 51%, 90%);
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
                  v-html="ticketData?.content"
                  style="
                    height: 116pt;
                    width: 768pt;
                    padding: 5px;
                    font-size: 10pt;
                  "
                  colspan="16"
                ></td>
              </tr>
              <tr>
                <td
                  style="
                    font-size: 10pt;
                    background-color: hsl(192, 51%, 90%);
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
                  v-html="ticketData?.reason"
                  style="
                    height: 101.5pt;
                    width: 768pt;
                    padding: 5px;
                    font-size: 10pt;
                  "
                  colspan="16"
                ></td>
              </tr>
              <tr>
                <td
                  style="
                    font-size: 10pt;
                    background-color: hsl(192, 51%, 90%);
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
                  v-html="ticketData?.other"
                  style="
                    height: 101.5pt;
                    width: 768pt;
                    padding: 5px;
                    font-size: 10pt;
                  "
                  colspan="16"
                ></td>
              </tr>
              <tr>
                <td
                  style="
                    font-size: 10pt;
                    background-color: hsl(192, 51%, 90%);
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
                <td style="height: 101.5pt; width: 768pt" colspan="16">
                  &nbsp;
                </td>
              </tr>
            </tbody>
          </table>
        </figure>
        <br />
      </div>
    </template>
    <template v-slot:footer>
      <div class="w-full">
        <div
          class="w-44 flex mx-auto"
          v-if="currentUser?.role?.name === ROLE.ADMIN"
        >
          <button class="button-primary mr-2">Done</button>
          <button class="button-transparent">Cancel</button>
        </div>
        <div
          v-if="currentSignatoryData?.status?.name === TICKET_STATUS.PENDING"
          class="w-44 flex mx-auto"
        >
          <button
            class="button-primary mr-2"
            :disabled="isProcessing"
            @click="approved"
          >
            {{ isProcessing ? "Approving..." : "Approved" }}
          </button>

          <button class="button-transparent" @click="openDeclineReasonModal">
            Declined
          </button>
        </div>

        <div
          v-if="ticketData?.status?.name === TICKET_STATUS.DONE"
          class="w-44 flex mx-auto"
        >
          <button class="button-primary mr-2">Download</button>
          <button class="button-transparent">Print</button>
        </div>
      </div>
    </template>
  </Modal>
</template>
<script>
import Modal from "@/components/Modal.vue";

import { useStore } from "vuex";
import { useRoute } from "vue-router";
import { ref, watch, computed } from "vue";
import { useSignalR } from "@quangdao/vue-signalr";
import { TICKET_STATUS, ROLE, JOB_TITLE } from "@/util/constant";
import { formatDate } from "@/util/helper";

export default {
  emits: ["close"],
  components: {
    Modal,
  },

  setup() {
    const signalR = useSignalR();
    const store = useStore();
    const route = useRoute();
    const ticketData = ref({});
    const currentSignatoryData = ref({});
    const signatories = ref([]);
    const currentUser = JSON.parse(localStorage.getItem("user"));
    const isProcessing = computed(() => store.state.app.isProcessing);

    const approved = async () => {
      const signatoryId = currentSignatoryData.value.signatory_id;
      const connectionId = signalR.connection.connectionId;

      const approver = {
        signatory_id: signatoryId,
        connection_id: connectionId,
      };

      await store.dispatch("ticket/approveTicket", approver);
    };

    const openDeclineReasonModal = () => {
      const signatoryId = currentSignatoryData.value.signatory_id;
      const connectionId = signalR.connection.connectionId;
      store.commit("app/SET_SIGNATORY", {
        signatoryId: signatoryId,
        connectionId: connectionId,
      });
      store.commit("app/SET_DECLINE_REASON_MODAL", true);
    };

    const getApprover = (jobTitle) => {
      if (ticketData.value?.subject) {
        return signatories.value.find(
          (s) => s.user.user.job_title.name === jobTitle
        )?.user?.user?.short_name;
      }
      return null;
    };

    const getCheckers = () => {
      return signatories.value.filter((c) => c.type === ROLE.CHECKER);
    };

    const getDate = (ticketStatus) => {
      return ticketData.value?.status?.name === ticketStatus
        ? formatDate(ticketData.value.action_date)
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
          signatories.value = store.state.ticket.ticket.signatories;
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
      getDate,
      getCheckers,
      approved,
      openDeclineReasonModal,
      getApprover,
    };
  },
};
</script>
