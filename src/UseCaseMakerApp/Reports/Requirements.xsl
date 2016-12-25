<?xml version="1.0"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
	<xsl:import href="MatchLink.xsl" />
 	<xsl:output method="html" />

  <xsl:param name="parentUniqueID" />
  <xsl:param name="requirements" />
  <xsl:param name="categoryNodeSet" />
	<xsl:param name="category" />
  <xsl:param name="importanceNodeSet" />
  <xsl:param name="importance" />
	<xsl:param name="acceptanceNodeSet" />
	<xsl:param name="acceptance" />
  <xsl:param name="statusNodeSet" />
  <xsl:param name="status" />
  <xsl:param name="historyTypeNodeSet" />
  <xsl:param name="history" />
  <xsl:param name="proposedBy" />
  <xsl:param name="benefitTo" />
  <xsl:param name="mappedUCs" />

  <xsl:template match="/">
		<html>
			<head>
				<link rel="stylesheet" type="text/css" href="ucm.css" />
			</head>
			<body>
        <xsl:apply-templates mode="page" select="//*[@UniqueID = $parentUniqueID]" />
			</body>
		</html>
	</xsl:template>

	<xsl:template mode="page" match="*">
    <h1 class="Title">
      <xsl:value-of select="$requirements" /> (<xsl:value-of select="@Name"/>)
    </h1>
		<xsl:apply-templates select="Requirements" />
	</xsl:template>
  
	<xsl:template match="Requirements">
    <xsl:for-each select="Requirement">
      <table align="center" class="Table" cellpadding="2" cellspacing="0" width="600px">
        <tr>
          <td class="HeaderTableCell" colspan="2">
            <xsl:value-of select="@Name" />
          </td>
        </tr>
        <tr>
		      <td class="TableCell" colspan="2">
			      <xsl:call-template name="MatchLink">
				      <xsl:with-param name="text" select="Description"/>
			      </xsl:call-template>
		      </td>
	      </tr>
      </table>
      <br></br>
      <xsl:if test="Proponents/*">
      <table align="center" class="Table" cellpadding="2" cellspacing="0" width="600px">
        <tr>
          <td class="HeaderTableCell">
            <xsl:value-of select="$proposedBy" />
          </td>
        </tr>
        <xsl:for-each select="Proponents/ReferencedObject">
        <tr>
          <td class="TableCell">
            <xsl:variable name="uid" select="UniqueID" />
            <xsl:variable name="target" select="concat('Stakeholders','.htm','#',$uid)" />
            <a class="href_stakeholder" href="{$target}" target="mainPage">
              <xsl:value-of select="//*[@UniqueID = $uid]/@Name" />
            </a>
          </td>
        </tr>
        </xsl:for-each>
      </table>
      <br></br>
      </xsl:if>
      <xsl:if test="Beneficiaries/*">
        <table align="center" class="Table" cellpadding="2" cellspacing="0" width="600px">
          <tr>
            <td class="HeaderTableCell">
              <xsl:value-of select="$benefitTo" />
            </td>
          </tr>
          <xsl:for-each select="Beneficiaries/ReferencedObject">
            <tr>
              <td class="TableCell">
                <xsl:variable name="uid" select="UniqueID" />
                <xsl:variable name="target" select="concat('Stakeholders','.htm','#',$uid)" />
                <a class="href_stakeholder" href="{$target}" target="mainPage">
                  <xsl:value-of select="//*[@UniqueID = $uid]/@Name" />
                </a>
              </td>
            </tr>
          </xsl:for-each>
        </table>
        <br></br>
      </xsl:if>
      <xsl:apply-templates select="Category" />
      <xsl:apply-templates select="Importance" />
      <xsl:apply-templates select="Status" />
      <xsl:apply-templates select="AcceptanceStatus" />
      <xsl:if test="MappedUseCases/*">
        <table align="center" class="Table" cellpadding="2" cellspacing="0" width="600px">
          <tr>
            <td class="HeaderTableCell">
              <xsl:value-of select="$mappedUCs" />
            </td>
          </tr>
          <xsl:for-each select="MappedUseCases/ReferencedObject">
            <tr>
              <td class="TableCell">
                <xsl:variable name="uid" select="UniqueID" />
                <a class="href_elementname" href="{$uid}.htm" target="mainPage">
                  <xsl:value-of select="//*[@UniqueID = $uid]/@Name" />
                </a>
              </td>
            </tr>
          </xsl:for-each>
        </table>
        <br></br>
      </xsl:if>
      <hr width="600px"></hr>
      <xsl:if test="HistoryItems/*">
        <xsl:apply-templates select="HistoryItems" />
      </xsl:if>
    </xsl:for-each>
	</xsl:template>

  <xsl:template match="Category">
    <xsl:variable name="target" select="text()" />
    <xsl:variable name="value" select="$categoryNodeSet[@EnumName = $target]" />
    <table align="center" class="Table" cellpadding="2" cellspacing="0" width="600px">
      <tr>
        <td class="HeaderTableCell" width="30%">
          <xsl:value-of select="$category" />
        </td>
      </tr>
      <tr>
        <td class="TableCell">
          <xsl:value-of select="$value" />
        </td>
      </tr>
    </table>
    <br></br>
  </xsl:template>

  <xsl:template match="Importance">
    <xsl:variable name="target" select="text()" />
    <xsl:variable name="value" select="$importanceNodeSet[@EnumName = $target]" />
    <table align="center" class="Table" cellpadding="2" cellspacing="0" width="600px">
      <tr>
        <td class="HeaderTableCell" width="30%">
          <xsl:value-of select="$importance" />
        </td>
      </tr>
      <tr>
        <td class="TableCell">
          <xsl:value-of select="$value" />
        </td>
      </tr>
    </table>
    <br></br>
  </xsl:template>

  <xsl:template match="Status">
    <xsl:variable name="target" select="text()" />
    <xsl:variable name="value" select="$statusNodeSet[@EnumName = $target]" />
    <table align="center" class="Table" cellpadding="2" cellspacing="0" width="600px">
      <tr>
        <td class="HeaderTableCell" width="30%">
          <xsl:value-of select="$status" />
        </td>
      </tr>
      <tr>
        <td class="TableCell">
          <xsl:value-of select="$value" />
        </td>
      </tr>
    </table>
    <br></br>
  </xsl:template>

  <xsl:template match="AcceptanceStatus">
    <xsl:variable name="target" select="text()" />
    <xsl:variable name="value" select="$acceptanceNodeSet[@EnumName = $target]" />
    <table align="center" class="Table" cellpadding="2" cellspacing="0" width="600px">
      <tr>
        <td class="HeaderTableCell" width="30%">
          <xsl:value-of select="$acceptance" />
        </td>
      </tr>
      <tr>
        <td class="TableCell">
          <xsl:value-of select="$value" />
        </td>
      </tr>
    </table>
    <br></br>
  </xsl:template>

  <xsl:template match="HistoryItems">
		<table align="center" class="Table" cellpadding="2" cellspacing="0" width="600px">
			<tr>
				<td class="HeaderTableCell" colspan="4">
					<xsl:value-of select="$history" />
				</td>
			</tr>
			<xsl:for-each select="HistoryItem">
			<tr>
				<td class="TableCell" width="100px" valign="top">
					<xsl:value-of select="substring-before(DateValue,' ')" />
				</td>
				<td class="TableCell">
					<xsl:variable name="typeTarget" select="Type/text()" />
					<xsl:variable name="typeValue" select="$historyTypeNodeSet[@EnumName = $typeTarget]" />
					<xsl:value-of select="$typeValue" />
				</td>
				<xsl:variable name="actionTarget" select="Action/text()" />
				<xsl:choose>
					<xsl:when test="Type/text() = 'Status'">
						<td class="TableCell">
							<xsl:variable name="statusValue" select="$statusNodeSet[@ListIndex = $actionTarget]" />
							<xsl:value-of select="$statusValue" />
						</td>
					</xsl:when>
          <xsl:when test="Type/text() = 'Acceptance'">
            <td class="TableCell">
              <xsl:variable name="acceptanceValue" select="$acceptanceNodeSet[@ListIndex = $actionTarget]" />
              <xsl:value-of select="$acceptanceValue" />
            </td>
          </xsl:when>
				</xsl:choose>
			</tr>
			</xsl:for-each>
		</table>
		<br></br>
		<hr width="600px"></hr>
	</xsl:template>
	
</xsl:stylesheet>