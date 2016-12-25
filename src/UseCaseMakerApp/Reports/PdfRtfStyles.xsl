<?xml version="1.0" encoding="UTF-8" ?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <!-- Start of Style Attributes -->
  <xsl:attribute-set name="ElementTitleCell">
    <xsl:attribute name="bgred">144</xsl:attribute>
    <xsl:attribute name="bggreen">144</xsl:attribute>
    <xsl:attribute name="bgblue">204</xsl:attribute>
    <xsl:attribute name="borderwidth">0.2</xsl:attribute>
    <xsl:attribute name="left">true</xsl:attribute>
    <xsl:attribute name="right">true</xsl:attribute>
    <xsl:attribute name="top">true</xsl:attribute>
    <xsl:attribute name="bottom">true</xsl:attribute>
    <xsl:attribute name="red">0</xsl:attribute>
    <xsl:attribute name="green">0</xsl:attribute>
    <xsl:attribute name="blue">0</xsl:attribute>
  </xsl:attribute-set>
  <xsl:attribute-set name="ElementTitle">
    <xsl:attribute name="font">Helvetica</xsl:attribute>
    <xsl:attribute name="size">12</xsl:attribute>
    <xsl:attribute name="fontstyle">bold</xsl:attribute>
    <xsl:attribute name="red">255</xsl:attribute>
    <xsl:attribute name="green">255</xsl:attribute>
    <xsl:attribute name="blue">255</xsl:attribute>
  </xsl:attribute-set>
  <xsl:attribute-set name="ElementNameCell">
    <xsl:attribute name="bgred">255</xsl:attribute>
    <xsl:attribute name="bggreen">255</xsl:attribute>
    <xsl:attribute name="bgblue">255</xsl:attribute>
    <xsl:attribute name="borderwidth">0.2</xsl:attribute>
    <xsl:attribute name="left">true</xsl:attribute>
    <xsl:attribute name="right">true</xsl:attribute>
    <xsl:attribute name="top">true</xsl:attribute>
    <xsl:attribute name="bottom">true</xsl:attribute>
    <xsl:attribute name="red">0</xsl:attribute>
    <xsl:attribute name="green">0</xsl:attribute>
    <xsl:attribute name="blue">0</xsl:attribute>
  </xsl:attribute-set>
  <xsl:attribute-set name="ElementName">
    <xsl:attribute name="red">0</xsl:attribute>
    <xsl:attribute name="green">0</xsl:attribute>
    <xsl:attribute name="blue">0</xsl:attribute>
    <xsl:attribute name="font">Helvetica</xsl:attribute>
    <xsl:attribute name="size">15.0</xsl:attribute>
    <xsl:attribute name="fontstyle">bold</xsl:attribute>
  </xsl:attribute-set>
  <xsl:attribute-set name="ElementTextCell">
    <xsl:attribute name="borderwidth">0.2</xsl:attribute>
    <xsl:attribute name="left">true</xsl:attribute>
    <xsl:attribute name="right">true</xsl:attribute>
    <xsl:attribute name="top">true</xsl:attribute>
    <xsl:attribute name="bottom">true</xsl:attribute>
    <xsl:attribute name="red">0</xsl:attribute>
    <xsl:attribute name="green">0</xsl:attribute>
    <xsl:attribute name="blue">0</xsl:attribute>
  </xsl:attribute-set>
  <xsl:attribute-set name="ElementText">
    <xsl:attribute name="font">Helvetica</xsl:attribute>
    <xsl:attribute name="size">9.0</xsl:attribute>
    <xsl:attribute name="red">0</xsl:attribute>
    <xsl:attribute name="green">0</xsl:attribute>
    <xsl:attribute name="blue">0</xsl:attribute>
  </xsl:attribute-set>
  <xsl:attribute-set name="ElementLinkCell">
    <xsl:attribute name="borderwidth">0.2</xsl:attribute>
    <xsl:attribute name="left">true</xsl:attribute>
    <xsl:attribute name="right">true</xsl:attribute>
    <xsl:attribute name="top">true</xsl:attribute>
    <xsl:attribute name="bottom">true</xsl:attribute>
    <xsl:attribute name="red">0</xsl:attribute>
    <xsl:attribute name="green">0</xsl:attribute>
    <xsl:attribute name="blue">0</xsl:attribute>
    <xsl:attribute name="font">Helvetica</xsl:attribute>
    <xsl:attribute name="size">9.0</xsl:attribute>
  </xsl:attribute-set>
  <xsl:attribute-set name="ElementLink">
    <xsl:attribute name="font">Helvetica</xsl:attribute>
    <xsl:attribute name="size">9.0</xsl:attribute>
    <xsl:attribute name="fontstyle">italic,underline</xsl:attribute>
    <xsl:attribute name="red">0</xsl:attribute>
    <xsl:attribute name="green">0</xsl:attribute>
    <xsl:attribute name="blue">0</xsl:attribute>
  </xsl:attribute-set>
  <xsl:attribute-set name="ParaSep">
    <xsl:attribute name="font">Helvetica</xsl:attribute>
    <xsl:attribute name="spacingafter">12.0</xsl:attribute>
    <xsl:attribute name="spacingbefore">12.0</xsl:attribute>
    <xsl:attribute name="borderwidth">0.3</xsl:attribute>
    <xsl:attribute name="left">false</xsl:attribute>
    <xsl:attribute name="right">false</xsl:attribute>
    <xsl:attribute name="top">false</xsl:attribute>
    <xsl:attribute name="bottom">true</xsl:attribute>
    <xsl:attribute name="red">128</xsl:attribute>
    <xsl:attribute name="green">128</xsl:attribute>
    <xsl:attribute name="blue">128</xsl:attribute>
    <xsl:attribute name="indentationleft">5.0</xsl:attribute>
    <xsl:attribute name="indentationright">5.0</xsl:attribute>
  </xsl:attribute-set>
  <xsl:attribute-set name="ParaTitle">
    <xsl:attribute name="align">left</xsl:attribute>
    <xsl:attribute name="font">Helvetica</xsl:attribute>
    <xsl:attribute name="size">15.0</xsl:attribute>
    <xsl:attribute name="fontstyle">bold</xsl:attribute>
    <xsl:attribute name="red">0</xsl:attribute>
    <xsl:attribute name="green">0</xsl:attribute>
    <xsl:attribute name="blue">0</xsl:attribute>
  </xsl:attribute-set>
  <xsl:attribute-set name="CoverText">
    <xsl:attribute name="red">144</xsl:attribute>
    <xsl:attribute name="green">144</xsl:attribute>
    <xsl:attribute name="blue">204</xsl:attribute>
    <xsl:attribute name="font">Helvetica</xsl:attribute>
    <xsl:attribute name="size">11.0</xsl:attribute>
    <xsl:attribute name="fontstyle">bold</xsl:attribute>
  </xsl:attribute-set>
  <xsl:attribute-set name="CoverTitle">
    <xsl:attribute name="red">0</xsl:attribute>
    <xsl:attribute name="green">0</xsl:attribute>
    <xsl:attribute name="blue">0</xsl:attribute>
    <xsl:attribute name="font">Helvetica</xsl:attribute>
    <xsl:attribute name="size">24.0</xsl:attribute>
    <xsl:attribute name="fontstyle">bold</xsl:attribute>    
  </xsl:attribute-set>
  <xsl:attribute-set name="CoverSubTitle">
    <xsl:attribute name="red">0</xsl:attribute>
    <xsl:attribute name="green">0</xsl:attribute>
    <xsl:attribute name="blue">0</xsl:attribute>
    <xsl:attribute name="font">Helvetica</xsl:attribute>
    <xsl:attribute name="size">15.0</xsl:attribute>
    <xsl:attribute name="fontstyle">bold</xsl:attribute>
  </xsl:attribute-set>
  <xsl:attribute-set name="CoverCell">
    <xsl:attribute name="horizontalalign">center</xsl:attribute>
    <xsl:attribute name="left">false</xsl:attribute>
    <xsl:attribute name="right">false</xsl:attribute>
    <xsl:attribute name="top">false</xsl:attribute>
    <xsl:attribute name="bottom">false</xsl:attribute>
    <xsl:attribute name="red">0</xsl:attribute>
    <xsl:attribute name="green">0</xsl:attribute>
    <xsl:attribute name="blue">0</xsl:attribute>
  </xsl:attribute-set>  
  <!-- End of Style Attributes -->
</xsl:stylesheet>