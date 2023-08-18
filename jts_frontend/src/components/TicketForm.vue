<template>
  <Modal @close="$emit('close')">
    <template v-slot:header>
      <h5 class="modal-title">{{ ticket?.ticket?.ticket_id }}</h5>
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
                  style="border: 2px solid hsl(0, 0%, 0%); width: 192pt"
                  colspan="4"
                >
                  <!--Approved Date-->
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
                    padding: 3px;
                    font-size: 8pt;
                    border: 2px solid hsl(0, 0%, 0%);
                    width: 96pt;
                  "
                  colspan="2"
                >
                  {{ requestedDate }}
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
                >
                  &nbsp;
                </td>
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
                <td style="width: 48pt">&nbsp;</td>
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
                    font-size: 8pt;
                    border: 2px solid hsl(0, 0%, 0%);
                    width: 284pt;
                    max-width: 284pt;
                  "
                  colspan="8"
                >
                  {{ ticket?.ticket?.subject }}
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
                    padding: 3px;
                    font-size: 8pt;
                  "
                  colspan="5"
                >
                  {{ ticket?.ticket?.condition }}
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
                    font-size: 8pt;
                    border: 2px solid hsl(0, 0%, 0%);
                    width: 144pt;
                  "
                  colspan="3"
                  rowspan="2"
                >
                  &nbsp;
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
                    padding: 5px;
                    font-size: 9pt;
                    border: 2px solid hsl(0, 0%, 0%);
                    width: 144pt;
                  "
                  colspan="3"
                  rowspan="2"
                >
                  {{ ticket?.ticket?.created_by?.user?.ext_name }}
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
                    font-size: 9pt;
                    border: 2px solid hsl(0, 0%, 0%);
                    width: 144pt;
                  "
                  colspan="3"
                  rowspan="2"
                >
                  {{ ticket?.ticket?.user?.department?.name }}
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
                  v-html="ticket?.ticket?.background"
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
                  v-html="ticket?.ticket?.content"
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
                  v-html="ticket?.ticket?.reason"
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
                  v-html="ticket?.ticket?.other"
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
        <div v-if="isPending && isSignatory" class="w-44 flex mx-auto">
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

        <div v-else class="w-44 flex mx-auto">
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
import { TICKET_STATUS, ROLE } from "@/util/constant";
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
    const ticket = ref({});
    const requestedDate = ref(null);
    const dateApproved = ref(null);
    const dateDeclined = ref(null);
    const currentUser = JSON.parse(localStorage.getItem("user"));
    const isPending = ref(false);
    const signatory = ref({});
    const isSignatory = ref(false);
    const isProcessing = computed(() => store.state.app.isProcessing);
    const signatoryStatus = ref(null);
    const actionDate = ref(null);

    const approved = async () => {
      const signatoryId = signatory.value.signatory_id;
      const connectionId = signalR.connection.connectionId;

      const approver = {
        signatory_id: signatoryId,
        connection_id: connectionId,
      };

      await store.dispatch("ticket/approveTicket", approver);
    };

    const openDeclineReasonModal = () => {
      const signatoryId = signatory.value.signatory_id;
      const connectionId = signalR.connection.connectionId;
      store.commit("app/SET_SIGNATORY", {
        signatoryId: signatoryId,
        connectionId: connectionId,
      });
      store.commit("app/SET_DECLINE_REASON_MODAL", true);
    };

    watch(
      () => route.params.ticketId,
      async (newTicketId, oldTicketId) => {
        if (newTicketId) {
          await store.dispatch("ticket/fetchTicket", newTicketId);
          const fetchedTicket = store.state.ticket.ticket;

          if (
            currentUser.roleModel.name == ROLE.ADMIN ||
            currentUser.roleModel.name == ROLE.CHECKER ||
            currentUser.roleModel.name == ROLE.APPROVER
          ) {
            isSignatory.value = true;
            signatory.value = fetchedTicket.signatories.find(
              (s) => s.user.user.user_id == currentUser.user_id
            );

            if (signatory.value != null) {
              signatoryStatus.value = signatory.value?.status?.name;
              actionDate.value = formatDate(signatory.value?.action_date);
            }

            isPending.value =
              signatory.value?.status?.name === TICKET_STATUS.PENDING;
          }
          ticket.value = fetchedTicket;

          requestedDate.value = formatDate(ticket.value.ticket.date_created);
          dateApproved.value = formatDate(ticket.value.ticket.date_approved);
          dateDeclined.value = formatDate(ticket.value.ticket.date_declined);
        }
      },
      { immediate: true }
    );

    return {
      ticket,
      requestedDate,
      isPending,
      isSignatory,
      dateApproved,
      isProcessing,
      signatoryStatus,
      TICKET_STATUS,
      actionDate,
      dateDeclined,
      approved,
      openDeclineReasonModal,
    };
  },
};
</script>
