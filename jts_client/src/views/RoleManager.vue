<template>
  <div class="ticket_form_container">
    <label>Date Requested</label>
    <div class="rf-detail-container">
      {{ requestedDate }}
    </div>

    <label>Prepared By</label>
    <div class="rf-detail-container">
      <div v-html="ticket?.ticket?.user?.ext_name"></div>
    </div>

    <label>Subject</label>
    <div class="rf-detail-container">
      {{ ticket?.ticket?.subject }}
    </div>
    <label>Background</label>
    <div class="rf-detail-container">
      <div v-html="ticket?.ticket?.background"></div>
    </div>
    <label>Contents</label>
    <div class="rf-detail-container">
      <div v-html="ticket?.ticket?.content"></div>
    </div>
    <label>Reasons</label>
    <div class="rf-detail-container">
      <div v-html="ticket?.ticket?.reason"></div>
    </div>
    <label>Others</label>
    <div class="rf-detail-container">
      <div v-html="ticket?.ticket?.others"></div>
    </div>
    <label>Attached Documents</label>
    <div class="rf-detail-container"></div>

    <div v-if="ticket?.ticket?.status?.name == TICKET_STATUS.APPROVED">
      <label>Date Approved</label>
      <div class="rf-detail-container">
        {{ dateApproved }}
      </div>
    </div>

    <div v-if="ticket?.ticket?.status?.name == TICKET_STATUS.DECLINED">
      <label>Date Declined</label>
      <div class="rf-detail-container">
        {{ dateDeclined }}
      </div>
    </div>

    <div v-if="signatoryStatus == TICKET_STATUS.APPROVED">
      <label>Date You Approved</label>
      <div class="rf-detail-container">
        {{ actionDate }}
      </div>
    </div>

    <div v-if="signatoryStatus == TICKET_STATUS.DECLINED">
      <label>Date You Declined</label>
      <div class="rf-detail-container">
        {{ actionDate }}
      </div>
    </div>
  </div>
  <div>
    <p><black>Decline By:</black> Jerico Rito</p>
    <p>Reason: dsdasdasd</p>
  </div>
</template>

<script>
export default {};
</script>
